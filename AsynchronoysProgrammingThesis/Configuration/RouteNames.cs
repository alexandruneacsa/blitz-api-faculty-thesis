namespace Blitz.API.Configuration
{
    public static class RouteNames
    {
        public const string SignupRoute = "auth:signup";
        public const string LoginRoute = "auth:login";

        public const string SendEmailsRoute = "email:send";

        public const string DownloadPicturesRoute = "image:download";
        public const string ReadImagesExternalRoute = "image:read";

        public const string DownloadVideosRoute = "video:download";
        public const string ReadVideosExternalRoute = "video:read";
        public const string UploadVideosExternalRoute = "video:upload";

        public const string LoadDocumentsRoute = "document:load";
    }
}
