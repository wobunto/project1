using MyBlazorApp.Components;
using MyGame.PokemonDatas;
using MyGame.Moves;
using MyGame.Items;
using MyGame.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ==========================================
// 2. 포켓몬 데이터베이스 로드 (앱 시작 시 1회 실행)
// ==========================================
PokemonDatabase.LoadPokemonDatabase();
MoveDatabase.LoadMoveDatabase();
ItemDatabase.LoadItemDatabase();

builder.Services.AddScoped<BattleGameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
