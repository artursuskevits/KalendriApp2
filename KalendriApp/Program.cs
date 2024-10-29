using KalenderApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������� � ���������
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // ���������� builder.Configuration
builder.Services.AddSession(); // �������� ��������� ������
builder.Services.AddRazorPages();

var app = builder.Build();

// ������������ ��������� HTTP-��������
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseSession(); // ��������� ������
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"); // ������������� Home/Index ��� ���������
    endpoints.MapRazorPages();
});


app.Run();
