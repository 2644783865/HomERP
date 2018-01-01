using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.Models.Shared
{
    public class PageInfo
    {
        private int currentPage = 1;
        public int CurrentPage
        {
            get
            {
                if (currentPage > Pages)
                {
                    return Pages;
                }
                else
                {
                    return currentPage;
                }
            }
            set
            {
                if (value < 1)
                {
                    currentPage = 1;
                }
                else
                {
                    currentPage = value;
                }
            }
        }
        public int TotalItems { get; set; }
        public int PageSize { get; set; } = 10;
        [JsonIgnore]
        public int Pages => (int)Math.Ceiling((float)TotalItems / PageSize);
        [JsonIgnore]
        public string Info
        {
            get
            {
                int firstDisplayed = (CurrentPage-1)*PageSize+1;
                int lastDisplayed = CurrentPage * PageSize;
                if (TotalItems<lastDisplayed)
                {
                    lastDisplayed = TotalItems;
                }

                if (firstDisplayed>lastDisplayed)
                {
                    return "";
                }
                return firstDisplayed.ToString() + "-" + lastDisplayed.ToString() + " z " + TotalItems.ToString();
            }
        }
        [JsonIgnore]
        public int RowsToSkip
        {
            get { return (CurrentPage - 1) * PageSize; }
        }

        public Message Message { get; set; }
        public string OrderField1 { get; set; }
        public string OrderField2 { get; set; }
        public OrderDirection Direction1 { get; set; }
        public OrderDirection Direction2 { get; set; }

        public PageInfo()
        { Message = new Message(); }

        public PageInfo ClearMessage()
        {
            return new PageInfo
            {
                TotalItems = this.TotalItems,
                PageSize = this.PageSize,
                CurrentPage = this.CurrentPage,
                Direction1 = this.Direction1,
                Direction2 = this.Direction2,
                Message = new Message(),
                OrderField1 = this.OrderField1,
                OrderField2 = this.OrderField2
            };
        }
    }
}
