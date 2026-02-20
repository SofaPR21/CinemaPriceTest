using CinemaTicketSystem;
using System.Net.Sockets;
namespace CalculatePriceTest
{
    public class CinemaTicketCLass
    {
        //обычный билета без скидок
        [Fact]
        public void CalculatePrice_Сhecking_ARegular_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 30;
            ticket.IsStudent = false;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(300, price);
        }

        //детский билет   
        [Fact]
        public void CalculatePrice_Сhecking_AChilde_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 5;
            ticket.IsStudent = false;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(0, price);
        }

        //студенческий билет
        [Fact]
        public void CalculatePrice_Сhecking_Student_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 18;
            ticket.IsStudent = true;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(240, price);
        }

        //пенсионный билет
        [Fact]
        public void CalculatePrice_Сhecking_Pension_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 67;
            ticket.IsStudent = false;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(150, price);
        }

        //билет со скидкой по средам
        [Fact]
        public void CalculatePrice_Сhecking_Wednesday_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 30;
            ticket.IsStudent = false;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Wednesday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(210, price);
        }

        //билет с утренней скидкой
        [Fact]
        public void CalculatePrice_Сhecking_Morning_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 30;
            ticket.IsStudent = false;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(10, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(255, price);
        }

        //VIP билет с наценкой
        [Fact]
        public void CalculatePrice_Сhecking_VIPRegular_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 30;
            ticket.IsStudent = false;
            ticket.IsVip = true;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(600, price);
        }

        //Несколько скидок одновременно (студенческий билет + c утра + в среду)
        [Fact]
        public void CalculatePrice_Сhecking_MultipleDiscounts_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 18;
            ticket.IsStudent = true;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Wednesday;
            ticket.SessionTime = new TimeSpan(10, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(105, price);
        }

        //Несколько скидок одновременно (Правило максимальной скидки)
        [Fact]
        public void CalculatePrice_Сhecking_MaxSale_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 18;
            ticket.IsStudent = true;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Wednesday;
            ticket.SessionTime = new TimeSpan(10, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(210, price);
        }

        //миниммально допустимый возраст
        [Fact]
        public void CalculatePrice_Сhecking_MinAge_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 0;
            ticket.IsStudent = false;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(0, price);
        }

        //Максимально допустимый возраст
        [Fact]
        public void CalculatePrice_Сhecking_MaxAge_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 120;
            ticket.IsStudent = false;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(150, price);
        }

        //возраст на границах скидок (6 лет детский билет)  
        [Fact]
        public void CalculatePrice_Сhecking_AgeLimitsOfDiscount_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 6;
            ticket.IsStudent = false;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            var price = calculator.CalculatePrice(ticket);
            Assert.Equal(180, price);
        }

        //все дынные равны null
        [Fact]
        public void CalculatePrice_Сhecking_NULL_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();

            Assert.Throws<ArgumentNullException>(
                () => calculator.CalculatePrice(null)
                );
        }

        //некорректный возраст (0<)
        [Fact]
        public void CalculatePrice_Сhecking_LessThanZero_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = -1;
            ticket.IsStudent = false;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            Assert.Throws<ArgumentOutOfRangeException>(
                () => calculator.CalculatePrice(ticket)
                );
        }

        //некорректный возраст (<120)
        [Fact]
        public void CalculatePrice_Сhecking_MoreThanZero_Ticket()
        {
            TicketPriceCalculator calculator = new TicketPriceCalculator();
            var ticket = new TicketRequest();
            ticket.Age = 121;
            ticket.IsStudent = false;
            ticket.IsVip = false;
            ticket.Day = DayOfWeek.Sunday;
            ticket.SessionTime = new TimeSpan(14, 0, 0);

            Assert.Throws<ArgumentOutOfRangeException>(
                () => calculator.CalculatePrice(ticket)
                );
        }
    }
}
