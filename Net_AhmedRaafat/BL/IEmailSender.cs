using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net_AhmedRaafat.BL
{
    public interface IEmailSender
    {
        bool SendEmail(string Subject,string Body,string From, string FromName, string To, string ToName,
                       string ReplyTo, string ReplyToName, IEnumerable<string> bcc, IEnumerable<string> cc,string AttachmentFilePath,
                       string AttachmentFileName,int AttachedDownloadId, IDictionary<string, string> headers
                       );
    }
}
