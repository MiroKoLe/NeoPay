using Microsoft.EntityFrameworkCore;
using NeoPay.Repository;
using NeoPay.Repository.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NeoPayContext>(options => {
    options.UseSqlServer("Data Source = MKOLEV\\SQLEXPRESS; Initial Catalog = NeoPay; Integrated Security = True");
    });

//DI registrations
builder.Services.AddScoped<IInvoiceStore, InvoiceStore>(); //isto kao i addSingleton, samo �to trajanje nije cijeli �ivotni vijek aplikacije nego samo dok se ne rije�i request. Request obi�no zavr�ava sa responsom. Kad se dogodi response sve instance se despose-aju. 
//builder.Services.AddSingleton<IInvoiceStore, InvoiceStore>(); //prvi puta kada se treba dependency stvara se instanca invoicestorea i ta instanca se koristi dokle god aplikacija �ivi. Svugdje gdje je dependency potreba shera se ta instanca. 
//builder.Services.AddTransient<IInvoiceStore, InvoiceStore>(); //svaki puta kada je dependency potreban stvara se nova instanca. Svugdje gdje se koristi di koristi se izolirana instanca te klase. 
//tri na�ina na koji se mo�e registrirati di.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
