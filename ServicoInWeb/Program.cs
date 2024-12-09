using ServicoInWeb.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Addinfra(builder.Configuration);

builder.Services.AddSession(o =>
{
	o.Cookie.HttpOnly = true;
	o.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Login}/{action=Index}"
);

app.UseStatusCodePagesWithReExecute("/Home/Error");

app.Run();
