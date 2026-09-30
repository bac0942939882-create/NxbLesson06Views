namespace Nxb.Services;

public class ImageUploadService(IWebHostEnvironment environment)
{
    private const long MaxSize = 5 * 1024 * 1024;
    private static readonly HashSet<string> Folders = new() { "Product", "Banner", "Avatar" };

    public async Task<string> SaveAsync(IFormFile file, string folder)
    {
        if (!Folders.Contains(folder)) throw new ArgumentException("Thư mục không hợp lệ.");
        if (file.Length <= 0 || file.Length > MaxSize)
            throw new InvalidDataException("Ảnh phải có dung lượng lớn hơn 0 và không quá 5 MB.");
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        byte[] header = new byte[12];
        await using var input = file.OpenReadStream();
        int count = await input.ReadAtLeastAsync(header, 12, throwOnEndOfStream: false);
        bool valid = extension switch
        {
            ".png" => count >= 8 && header.AsSpan(0, 8).SequenceEqual(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }),
            ".jpg" or ".jpeg" => count >= 3 && header[0] == 255 && header[1] == 216 && header[2] == 255,
            ".gif" => count >= 6 && (System.Text.Encoding.ASCII.GetString(header, 0, 6) is "GIF87a" or "GIF89a"),
            ".webp" => count >= 12 && System.Text.Encoding.ASCII.GetString(header, 0, 4) == "RIFF" && System.Text.Encoding.ASCII.GetString(header, 8, 4) == "WEBP",
            _ => false
        };
        if (!valid) throw new InvalidDataException("Chỉ nhận ảnh JPG, PNG, GIF hoặc WebP có nội dung hợp lệ.");
        // Tên do server tạo, tránh trùng file và đường dẫn tùy ý từ người dùng.
        string filename = $"{Guid.NewGuid():N}-upload{extension}";
        string directory = Path.Combine(environment.WebRootPath, folder);
        Directory.CreateDirectory(directory);
        string path = Path.Combine(directory, filename);
        try
        {
            await using var output = new FileStream(path, FileMode.CreateNew);
            await output.WriteAsync(header.AsMemory(0, count));
            await input.CopyToAsync(output);
        }
        catch
        {
            if (File.Exists(path)) File.Delete(path);
            throw;
        }
        return filename;
    }

    public void Delete(string? filename, string folder)
    {
        // Ảnh mẫu có thể được nhiều bản ghi cùng dùng, chỉ xóa ảnh đã upload.
        if (!Folders.Contains(folder) || string.IsNullOrWhiteSpace(filename) ||
            Path.GetFileName(filename) != filename || !Path.GetFileNameWithoutExtension(filename).EndsWith("-upload")) return;
        var path = Path.Combine(environment.WebRootPath, folder, filename);
        if (File.Exists(path)) File.Delete(path);
    }
}
