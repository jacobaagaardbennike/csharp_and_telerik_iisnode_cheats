using System;
using Telerik.Web.UI;
using App.Web.Services;

public partial class RestAfstem : System.Web.UI.Page 
{
    private RestAfstemService s = new RestAfstemService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGrid();
            SetSaldoLabels();
        }
    }

    protected void Grid_Bank_PreRender(object sender, EventArgs e)
    {
        Grid_Bank.DataSource = s.GetBankData();
        Grid_Bank.Rebind();
    }

    protected void Grid_Erp_PreRender(object sender, EventArgs e)
    {
        Grid_Erp.DataSource = s.GetErpData();
        Grid_Erp.Rebind();
    }

    protected void LoadGrid()
    {
        Grid_Bank.DataSource = s.GetBankData();
        Grid_Bank.Rebind();
        Grid_Erp.DataSource = s.GetErpData();
        Grid_Erp.Rebind();
    }


    private void SetSaldoLabels()
    {
        lbl_banksaldo.InnerText = s.GetBankSaldo();
        lbl_erpsaldo.InnerText = s.GetErpSaldo();
        lbl_saldoDifference.InnerText = s.GetSaldoDifference();
    }

    protected void btnAfstem_Click(object sender, EventArgs e)
    {
        if (IsInputValid())
        {
            if (Grid_Bank.SelectedItems.Count > 0 || Grid_Erp.SelectedItems.Count > 0)
            {
                s.Afstem(Grid_Bank.SelectedItems, Grid_Erp.SelectedItems);
            }

            lblErr.Text = "";
            LoadGrid();
        }
        else
        {
            lblErr.Text = "Beløb stemmer ikke";
        }
    }

    private bool IsInputValid()
    {
        decimal erp = 0;
        decimal bank = 0;
        foreach (GridDataItem ei in Grid_Erp.SelectedItems)
        {
            if (ei.GetDataKeyValue("amount") != null)
            {
                erp = erp + Convert.ToDecimal(ei.GetDataKeyValue("amount").ToString());
            }
            else
            {
                return false;
            }
        }

        foreach (GridDataItem bi in Grid_Bank.SelectedItems)
        {
            if (bi.GetDataKeyValue("amount") != null)
            {
                bank = bank + Convert.ToDecimal(bi.GetDataKeyValue("amount").ToString());
            }
            else
            {
                return false;
            }
        }
        return bank - erp == 0;
    }
}
