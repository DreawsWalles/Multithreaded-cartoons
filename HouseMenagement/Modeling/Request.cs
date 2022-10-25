using System;

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace Modeling
{
    //Заявка ЖКХ
    public abstract class Request
    {
        private int _requestId;

        public int RequestId
        {
            get => _requestId;
            set
            {
                if (value < 0)
                    throw new ArgumentException("RequestId");
                _requestId = value;
            }
        }

        public bool IsCompleted { get; set; }

        protected Request(int requestId)
        {
            RequestId = requestId;
            IsCompleted = false;
        }
    }

    //Заявка к сантехнику
    public class PlumberRequest : Request
    {
        public PlumberRequest(int requestId) : base(requestId)
        {
        }

        public override string ToString()
        {
            return $"Заявка №{RequestId} к сантехнику";
        }
    }

    //Заявка к электрику
    public class ElectricianRequest : Request
    {
        public ElectricianRequest(int requestId) : base(requestId)
        {
        }

        public override string ToString()
        {
            return $"Заявка №{RequestId} к электрику";
        }
    }

    //Заявка к уборщику
    public class JanitorRequest : Request
    {
        public JanitorRequest(int requestId) : base(requestId)
        {
        }

        public override string ToString()
        {
            return $"Заявка №{RequestId} к уборщику";
        }
    }
}