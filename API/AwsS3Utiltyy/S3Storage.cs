using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace MyDbWebApi.AwsS3Utiltyy
{
    public static class S3Storage
    {


        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];

        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

        private static readonly string _bucketName = ConfigurationManager.AppSettings["BucketName"];

        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;

        private static IAmazonS3 s3Client;

        public static bool  UploadFile(string base64String, string fileName )
        {
            bool isFileUploaded = false;

            try
            {
                 
                byte[] bytes = Convert.FromBase64String(base64String);

                using (s3Client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, bucketRegion))
                {
                   

                    var request = new PutObjectRequest
                    {
                        BucketName = _bucketName,
                        CannedACL = S3CannedACL.Private,
                        Key = string.Format("RH/{0}", fileName),                     

                    };
                    using (var ms = new MemoryStream(bytes))
                    {
                        request.InputStream = ms;

                       s3Client.PutObject(request);

                         isFileUploaded = true;
                    }
                }// use UploadAsync if possible
            }
            catch (Exception exception)
            {
                  isFileUploaded = false;
                // throw new S3UploadException(exception.Message);
            }
            return isFileUploaded;

        }

       public static string GeneratePreSignedURL( string fileName)
        {
            string urlString = "";
            try
            {


                using (s3Client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, bucketRegion))
                {


                    GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                    {
                        BucketName = _bucketName,
                        Key = string.Format("RH/{0}", fileName) ,
                        Expires = DateTime.Now.AddMinutes(5)
                    };
                    urlString = s3Client.GetPreSignedURL(request1);
                }
        
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            return urlString;
        }

        //private static async Task UploadObjectAsync(string base64String, string fileName)
        //{
        //    // Create list to store upload part responses.
        //    List<UploadPartResponse> uploadResponses = new List<UploadPartResponse>();

        //    // Setup information required to initiate the multipart upload.
        //    InitiateMultipartUploadRequest initiateRequest = new InitiateMultipartUploadRequest
        //    {
        //        BucketName = _bucketName,
        //        Key = fileName
        //    };

        //    // Initiate the upload.
        //    InitiateMultipartUploadResponse initResponse =
        //        await s3Client.InitiateMultipartUploadAsync(initiateRequest);

        //    // Upload parts.
        //    long contentLength = new FileInfo(filePath).Length;
        //    long partSize = 5 * (long)Math.Pow(2, 20); // 5 MB

        //    try
        //    {
        //        Console.WriteLine("Uploading parts");

        //        long filePosition = 0;
        //        for (int i = 1; filePosition < contentLength; i++)
        //        {
        //            UploadPartRequest uploadRequest = new UploadPartRequest
        //            {
        //                BucketName = bucketName,
        //                Key = keyName,
        //                UploadId = initResponse.UploadId,
        //                PartNumber = i,
        //                PartSize = partSize,
        //                FilePosition = filePosition,
        //                FilePath = filePath
        //            };

        //            // Track upload progress.
        //            uploadRequest.StreamTransferProgress +=
        //                new EventHandler<StreamTransferProgressArgs>(UploadPartProgressEventCallback);

        //            // Upload a part and add the response to our list.
        //            uploadResponses.Add(await s3Client.UploadPartAsync(uploadRequest));

        //            filePosition += partSize;
        //        }

        //        // Setup to complete the upload.
        //        CompleteMultipartUploadRequest completeRequest = new CompleteMultipartUploadRequest
        //        {
        //            BucketName = bucketName,
        //            Key = keyName,
        //            UploadId = initResponse.UploadId
        //        };
        //        completeRequest.AddPartETags(uploadResponses);

        //        // Complete the upload.
        //        CompleteMultipartUploadResponse completeUploadResponse =
        //            await s3Client.CompleteMultipartUploadAsync(completeRequest);
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine("An AmazonS3Exception was thrown: { 0}", exception.Message);

        //        // Abort the upload.
        //        AbortMultipartUploadRequest abortMPURequest = new AbortMultipartUploadRequest
        //        {
        //            BucketName = bucketName,
        //            Key = keyName,
        //            UploadId = initResponse.UploadId
        //        };
        //        await s3Client.AbortMultipartUploadAsync(abortMPURequest);
        //    }
        //}
        //public static void UploadPartProgressEventCallback(object sender, StreamTransferProgressArgs e)
        //{
        //    // Process event. 
        //    Console.WriteLine("{0}/{1}", e.TransferredBytes, e.TotalBytes);
        //}


    }
}