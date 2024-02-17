using MedicalSystem.Models;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using QRCoder;
using MedicalSystem.Infrastructure;

namespace MedicalSystem.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MainContext _context;

		public HomeController(ILogger<HomeController> logger, MainContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}
		public IActionResult GetMedCards()
		{
			var medCards = _context.MedCards.ToList();
			return Json(medCards);
		}
		public IActionResult AddMedCard([FromBody] PacientViewModel viewModel)
		{
			var medCard = new MedCard
			{
				PacientName = viewModel.Name,
				ImageBase64 = ""
			};
			_context.MedCards.Add(medCard);
			_context.SaveChanges();
			QRCodeGenerator codeGenerator = new QRCodeGenerator();
			var data = codeGenerator.CreateQrCode($"MedCards:{medCard.Id}", QRCodeGenerator.ECCLevel.Q);
			PngByteQRCode code = new PngByteQRCode(data);
			medCard.ImageBase64 = Convert.ToBase64String(code.GetGraphic(8));
			_context.SaveChanges();
			return Ok();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
