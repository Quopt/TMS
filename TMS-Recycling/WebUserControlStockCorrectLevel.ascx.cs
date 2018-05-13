using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlStockCorrectLevel : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "Material";

            if (!IsPostBack)
            {
                KeyID = new Guid(Request.Params["Id"]);

                Material Mat = (DataItem as Material);
                LabelMaterial.Text = Mat.Description;
                TextBoxPrice.Text = Mat.PurchasePrice.ToString();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

        }
        
        protected void RadioButtonListBuyOrSell_SelectedIndexChanged(object sender, EventArgs e)
        {
            Material Mat = (DataItem as Material);
            TextBoxPrice.Text = Mat.PurchasePrice.ToString();
            if (RadioButtonListBuyOrSell.SelectedValue == "Sell")
            {
                TextBoxPrice.Text = Mat.SalesPrice.ToString();
            }
        }

        protected void ButtonProcess_Click(object sender, EventArgs e)
        {
            Double MatPrice=0, MatAmount=0;

            try
            {
                MatPrice = Convert.ToDouble(TextBoxPrice.Text);
                MatAmount = Convert.ToDouble(TextBoxAmount.Text);
            }
            catch
            {
            }

            if ((MatPrice.ToString() != TextBoxPrice.Text) || (MatAmount.ToString() != TextBoxAmount.Text))
            {
                Common.InformUser(Page, "De opgegeven getallen voor prijs en hoeveelheid kunnen niet worden herkend. Corrigeer deze naar de juiste waarde.");
                TextBoxPrice.Text = MatPrice.ToString();
                TextBoxAmount.Text = MatAmount.ToString();
            }
            else
            {
                // process this as a Material Mutation
                MaterialMutation mt = new MaterialMutation();

                mt.Material = (DataItem as Material);
                mt.Description = "CORR / Correctie voorraad " + mt.Material.Description + " dd " + mt.CreateDateTime.ToString();
                mt.MutationType = RadioButtonListBuyOrSell.SelectedValue;
                mt.Amount = MatAmount;
                mt.PricePerUnit = MatPrice;
                mt.TotalPrice = MatPrice * mt.Amount;
                mt.GenerateMutationNumber(ControlObjectContext);

                if (mt.MutationType == "Sell")
                {
                    mt.Amount = -mt.Amount; // sales are always a negative amount
                }

                mt.RecalcTotals();
                mt.ProcessToStockLevel(false); // do not update average prices
                mt.ProcessToLedgerMutation(ControlObjectContext);

                if (mt.MutationType == "Sell")
                {
                    mt.TotalPrice = MatPrice * mt.Amount; // sales are stored as a negative price so that the total calculation will be correct on the daily closure
                }

                ControlObjectContext.AddToMaterialMutationSet(mt);
                ControlObjectContext.SaveChanges();
                Common.InformUser(Page, "Deze mutatie is succesvol doorgevoerd. U kunt dit popup scherm nu sluiten of nog een mutatie op dit materiaal doorvoeren.");
            }
        }
    }
}