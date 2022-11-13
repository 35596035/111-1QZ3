# 第3次隨堂-隨堂-QZ3
>
>學號：109111101
><br />
>姓名：邱韋翔
><br />
>作業撰寫時間：5 (mins，包含程式撰寫時間)
><br />
>最後撰寫文件日期：2022/11/13
>

本份文件包含以下主題：(至少需下面兩項，若是有多者可以自行新增)
- [x]說明內容
- [x]個人認為完成作業須具備觀念

## 說明程式與內容

根據題目要求從工具箱拉出對應的物件，要注意的是要先再建立.aspx檔來讓按下送出紐跳到其頁面，
所以需將Button的按鈕加上PostBakeUrl來導向該網頁
下段程式碼則為使用後結果：

Test.aspx
```html
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="_111_1QZ3.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>保單電訪紀錄</h1>
            紀錄編號:<asp:TextBox ID="tb_Num" runat="server"></asp:TextBox></br>
            聯絡方式:<asp:RadioButtonList ID="rbl_Phone" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rbl_Phone_SelectedIndexChanged">
                <asp:ListItem Selected="True">手機</asp:ListItem>
                <asp:ListItem>市話</asp:ListItem>
                <asp:ListItem>無</asp:ListItem>
            </asp:RadioButtonList></br>
            <asp:TextBox ID="txt_Phone" runat="server"></asp:TextBox></br></br>
            縣市與區域:<asp:DropDownList ID="dpl_City" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dpl_City_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="dpl_Area" runat="server"></asp:DropDownList></br>
            <asp:Button ID="btn_Submit" runat="server" Text="送出" Width="80px" PostBackUrl="Test_Sub.aspx" />
        </div>
    </form>
</body>
```
Test.aspx.cs
給予兩個String型別的list再運用for迴圈來抓取陣列裡存放的文字並給予
DropDownList下拉式選單來讓選單裡有選項出現，下列程式碼中的自己建立的方法mt_GenSecondList就是讓下拉式選單有選項出現
，至於Page_Load裡判斷IsPostBack則是讓頁面當選則其他縣市時頁面會在自動更新一次以此抓取該縣市的地區。
再根據題目要求當聯絡欄方式選擇無時輸入框的可視度變成false
透過if的方式來進行判斷。下列程式碼中的自己建立的方法
```csharp
namespace _111_1QZ3
{
    public partial class Test : System.Web.UI.Page
    {
        string[] s_City = new string[3] { "台北市", "新北市", "台中市" };
        string[,] s_Area = new string[3, 3] {
            {"中正區", "文山區", "大安區"},
            {"淡水區", "石碇區", "土城區"},
            {"西屯區", "北屯區", "東區"}
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i_Ct = 0; i_Ct < s_City.Length; i_Ct++)
                {
                    ListItem o_L = new ListItem();
                    o_L.Text = o_L.Value = s_City[i_Ct];

                    dpl_City.Items.Add(o_L);
                }
            }
            mt_GenSecondList();
        }
        protected void dpl_City_SelectedIndexChanged(object sender, EventArgs e)
        {
            mt_GenSecondList();
        }

        protected void mt_GenSecondList()
        {
            int i_ind = dpl_City.SelectedIndex;
            dpl_Area.Items.Clear();
            for (int i_Ct = 0; i_Ct < s_Area.GetLength(1); i_Ct++)//sa_2D.GetLength(1)是抓取sa_2D[(0),(1)]
            {
                ListItem o_L = new ListItem();
                o_L.Text = o_L.Value = s_Area[i_ind, i_Ct];
                dpl_Area.Items.Add(o_L);
            }
        }

        protected void rbl_Phone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbl_Phone.SelectedIndex == 2)
            {
                txt_Phone.Visible = false;
            }
            else
            {
                txt_Phone.Visible = true;
            }
        }
    }
}
```
Test_Sub.aspx
為Test.aspx裡按下按紐跳轉的網頁，依題目要求加入Label物件
```html
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_Sub.aspx.cs" Inherits="_111_1QZ3.Test_Sub" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lb_Msg" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
```
Test_Sub.aspx.cs
為Test.aspx裡按下按紐跳轉的網頁，該網頁主要抓取Test的資料，用指令Request.Form.Get()括號裡填寫的是需要Test.aspx那些物件的ID
因而抓到該ID底下的值，此部分我運用兩個String陣列來存放文字，一個是我要抓Test.aspx的ID的資料另個是輸出文字的部分，
運用for迴圈再裡面再加入if判斷當聯絡方式選擇無時底下的TextBox自然也變成虛值，所以我判斷當選擇為無時略過一次
```csharp
namespace _111_1QZ3
{
    public partial class Test_Sub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] s_Get = new string[] { "tb_Num", "rbl_Phone", "txt_Phone", "dpl_City", "dpl_Area" };
            string[] s_Menu = new string[] { "保單號碼: ", "通訊種類: ", "通訊號碼: ", "所在城市: ", "所在區域: " };
            for (int i_Ct = 0; i_Ct < s_Get.Length; i_Ct++)
            {
                if (Request.Form.Get(s_Get[i_Ct]) == null)
                {
                    continue;
                }
                lb_Msg.Text += s_Menu[i_Ct] + Request.Form.Get(s_Get[i_Ct]) + "<br />";
                Response.Write(Request.Form.Get(s_Get[i_Ct]));
            }
        }
    }
}
```
若要於內文中標示部分.aspx檔，則使用以下標籤` ```html 程式碼 ``` `，
下段程式碼則為使用後結果：


## 個人認為完成作業須具備觀念

本次須了解跳轉頁面的連結如何設定，和PodtBack的特性，來讓執行時的PostBack處於True的狀態，
另外就是在跳轉的頁面如何使用指令去抓取原頁面的值

