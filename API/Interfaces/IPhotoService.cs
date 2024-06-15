using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file); // Metodo para subir fotos
        Task<DeletionResult> DeletePhotoAsync(string publicId); // Metodo para borrar fotos, se relaciona con el publicId que estaba en Photo.cs, en entities



    }
}