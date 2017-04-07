# SearchModel

*Project Description*
{project:description}

You can use the code like this in ASP.NET MVC View
{code:aspx c#}
<form action="" method="post">
Name：@Html.TextBox("Name").ForSearch(QueryMethod.Like)   
Email：@Html.TextBox("Email").ForSearch(QueryMethod.Equal)<br />
Id: @Html.TextBox("Id").ForSearch(QueryMethod.Equal)
Birth: @Html.TextBox("Birthday").ForSearch(QueryMethod.Equal)<br />
<input type="submit" value="search" />
</form>
@{
   var grid = new WebGrid(Model);
   @grid.GetHtml();
}
{code:aspx c#}
And use the code in Controller 
{code:C#}
        public ActionResult Index(QueryModel model)
        {
            using(var db=new DbEntities())
            {
                var list = db.Users.Where(model).ToList();
                return View(list);
            }
        }
{code:C#}
To convert the form field to the entity framework query expression.

[url:@chsword on Weibo.com|http://weibo.com/chsword]
[url:linkedin|http://lnkd.in/b6vqP_c]
