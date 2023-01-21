using Microsoft.Reporting.WinForms;
using ShipTo.Reporting.VMs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShipTo.Reporting.Reports
{
    public partial class Form1 : Form
    {
        //ShipTo.Infrastructure.Repositories
        //ShippingOrderRepository shippingOrderRepository
        public Form1()
        {
            InitializeComponent();
        }

        List<ShippingOrderCarrierFileVM> LoadData()
        {
            ShippingOrderCarrierFileVM shippingOrder = new ShippingOrderCarrierFileVM()
            {
                CompanyName = "الحسن والحسين",
                CompanyLogo =    System.IO.Directory.GetCurrentDirectory().Replace(@"\bin\Debug", @"\Photos\CompanyLogo.jpg") + "",
                OrderNumber = "100000000023",
                ClientName = "اسلام علي محمد",
                ClientPhoneNumber = "01223443554",
                Governorate = "بني سويف",
                Address = "بني سويف/ناصر/الشانوية/شارع البوسطه",
                ShipperName = "دريسنج",
                OrderDetails = @"ليزر:كعب 39 و 40 للاختيار 
                                    كعب  39
                                    كعب 41
                                    فلات مقاس 39 او40 للاختيار",
                OrderTotalPrice = 650,
                DeliveryPrice = 0,
                ShippingPrice = 30,
                OrderNetPrice = 620,
                DeliveryStatusName = "قيد التسليم",
                DeliveryStatusReason = null,
                Notes = null,
                CarrierName = "احمد محمد مصطفي",
            };
            List<ShippingOrderCarrierFileVM> shippingOrders = new List<ShippingOrderCarrierFileVM>();
            shippingOrders.Add(shippingOrder);
            return shippingOrders;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = System.IO.Directory.GetCurrentDirectory().Replace(@"\bin\Debug", "") + @"\Reports\dc_ShippingOrderInvoice.rdlc";
            ReportDataSource datasource = new ReportDataSource("DS_ShippingOrderInvoice", LoadData());
            reportViewer1.LocalReport.DataSources.Clear();
            
            //reportViewer1.LocalReport.EnableExternalImages = true;
            //reportViewer1.LocalReport.SetParameters(new ReportParameter[] {
            //    new ReportParameter("CompanyName", "الحسن والحسين"),
            //    new ReportParameter("CompanyLogo", LogoPath),
            //    });
            reportViewer1.LocalReport.DataSources.Add(datasource);
            this.reportViewer1.RefreshReport();

            this.reportViewer1.RefreshReport();
        }
    }

    
}
