using System;
using System.Collections.Generic;
using System.Text;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public static class SmsSender
    {
        //产品名称:云通信短信API产品,开发者无需替换
        const String product = "Dysmsapi";
        //产品域名,开发者无需替换
        const String domain = "dysmsapi.aliyuncs.com";

        // TODO 此处需要替换成开发者自己的AK(在阿里云访问控制台寻找)
        const String accessKeyId = "LTAIJMLTEPv5DIyl";
        const String accessKeySecret = "9jhHmRafKgZjSobHYTar3Qfm12Db9w";
        const String signName = "BOOK2";
        const String templateCode = "SMS_150574704";

        public static Task<SendSmsResponse> Send(string phoneNumber, string code)
        {
            return Task.Run(() =>
            {
                IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
                DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
                IAcsClient acsClient = new DefaultAcsClient(profile);
                SendSmsRequest request = new SendSmsRequest();
                SendSmsResponse response = null;
                try
                {

                    //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                    request.PhoneNumbers = phoneNumber;
                    //必填:短信签名-可在短信控制台中找到
                    request.SignName = signName;
                    //必填:短信模板-可在短信控制台中找到
                    request.TemplateCode = templateCode;
                    //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
                    request.TemplateParam = "{\"code\":\"" + code + "\"}";
                    //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
                    request.OutId = "yourOutId";
                    //请求失败这里会抛ClientException异常
                    response = acsClient.GetAcsResponse(request);

                }
                catch (ServerException e)
                {
                    Console.WriteLine(e.ErrorCode);
                    throw e;
                }
                catch (ClientException e)
                {
                    Console.WriteLine(e.ErrorCode);
                    throw e;
                }
                return response;

            });
        }


        public static QuerySendDetailsResponse querySendDetails(String bizId)
        {
            //初始化acsClient,暂不支持region化
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            //组装请求对象
            QuerySendDetailsRequest request = new QuerySendDetailsRequest();
            //必填-号码
            request.PhoneNumber = "15000000000";
            //可选-流水号
            request.BizId = bizId;
            //必填-发送日期 支持30天内记录查询，格式yyyyMMdd       
            request.SendDate = DateTime.Now.ToString("yyyyMMdd");
            //必填-页大小
            request.PageSize = 10;
            //必填-当前页码从1开始计数
            request.CurrentPage = 1;

            QuerySendDetailsResponse querySendDetailsResponse = null;
            try
            {
                querySendDetailsResponse = acsClient.GetAcsResponse(request);
            }
            catch (ServerException e)
            {
                Console.WriteLine(e.ErrorCode);
            }
            catch (ClientException e)
            {
                Console.WriteLine(e.ErrorCode);
            }
            return querySendDetailsResponse;
        }

        //static void Main(string[] args)
        //{
        //    SendSmsResponse reponse = this.();
        //    Console.Write("短信发送接口返回的结果----------------");
        //    Console.WriteLine("Code=" + reponse.Code);
        //    Console.WriteLine("Message=" + reponse.Message);
        //    Console.WriteLine("RequestId=" + reponse.RequestId);
        //    Console.WriteLine("BizId=" + reponse.BizId);
        //    Console.WriteLine();
        //    Thread.Sleep(3000);

        //    if (reponse.Code != null && reponse.Code == "OK")
        //    {
        //        QuerySendDetailsResponse queryReponse = querySendDetails(reponse.BizId);

        //        Console.WriteLine("短信明细查询接口返回数据----------------");
        //        Console.WriteLine("Code=" + queryReponse.Code);
        //        Console.WriteLine("Message=" + queryReponse.Message);
        //        foreach (QuerySendDetailsResponse.SmsSendDetailDTO smsSendDetailDTO in queryReponse.SmsSendDetailDTOs)
        //        {
        //            Console.WriteLine("Content=" + smsSendDetailDTO.Content);
        //            Console.WriteLine("ErrCode=" + smsSendDetailDTO.ErrCode);
        //            Console.WriteLine("OutId=" + smsSendDetailDTO.OutId);
        //            Console.WriteLine("PhoneNum=" + smsSendDetailDTO.PhoneNum);
        //            Console.WriteLine("ReceiveDate=" + smsSendDetailDTO.ReceiveDate);
        //            Console.WriteLine("SendDate=" + smsSendDetailDTO.SendDate);
        //            Console.WriteLine("SendStatus=" + smsSendDetailDTO.SendStatus);
        //            Console.WriteLine("Template=" + smsSendDetailDTO.TemplateCode);
        //        }
        //    }

        //}
    }
}

