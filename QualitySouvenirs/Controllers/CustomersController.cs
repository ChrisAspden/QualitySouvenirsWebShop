using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using MimeKit;
using QualitySouvenirs.Models.Login;

namespace QualitySouvenirs.Controllers
{
    public class CustomersController : Controller
    {
        private readonly SouvenirContext _context;

        public CustomersController(SouvenirContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult RegisterConfirmation()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        public IActionResult LoginFail()
        {
            return View();
        }

        public IActionResult MyAccount(LoginViewModel model)
        {
            return View();
        }


        [HttpPost]
        public LoginViewModel Login(LoginViewModel model)
        {
            Customer customer;
            try
            {
                customer = _context.Customers.Where(c => c.Email == model.Email).Where(c => c.Password == model.Password).Single();
            }
            catch (InvalidOperationException ex)
            {
                return model;
            }
            customer = _context.Customers.Where(c => c.Email == model.Email).Where(c => c.Password == model.Password).Single();
            model.Email = customer.Email;
            model.Password = customer.Password;

            return model;
        }


        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,HomePhoneNumber,WorkPhoneNumber,MobilePhoneNumber,FirstMidName,LastName,Address,Email,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Chris", "chris.aspden.developer@gmail.com"));
                message.To.Add(new MailboxAddress("Quality Souvenirs", customer.Email));
                message.Subject = "Your Quality Souvenirs account";
                message.Body = new TextPart("plain")
                {
                    Text = "Thanks for registering with Quality Souvenirs" + Environment.NewLine + Environment.NewLine + 
                    "Your login is " + customer.Email + Environment.NewLine +Environment.NewLine + "Your password is " + customer.Password
                     + Environment.NewLine + Environment.NewLine + "Kindest Regards" + Environment.NewLine + Environment.NewLine + "Quality Souvenirs"
                };

                using(var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("chris.aspden.developer@gmail.com", "W3sb24da");
                    client.Send(message);
                    client.Disconnect(true);
                }
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RegisterConfirmation));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,HomePhoneNumber,WorkPhoneNumber,MobilePhoneNumber,FirstMidName,LastName,Address,Email,Password")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
