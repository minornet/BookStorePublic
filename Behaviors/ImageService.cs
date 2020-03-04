using System.Net;

namespace BookStore.Behaviors
{
    public class ImageService
    {

        // this isn't working.  response always comes back null

        public bool IsImage(string imgUrl)

        {
            var request = HttpWebRequest.Create(imgUrl);
            request.Method = "HEAD";
            
            bool isImage = true;

            WebResponse response = null;

            try
            {
               response = request.GetResponse();
            }
            catch (WebException ex)
            {
                /* A WebException will be thrown if the status of the response is not `200 OK` */
                var exBaseException = ex.GetBaseException();

                HttpWebResponse webResponse = (HttpWebResponse)ex.Response;

                isImage = false;
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null)
                {
                    response.Close();
                }
            }
            return isImage;
        }
    }
}