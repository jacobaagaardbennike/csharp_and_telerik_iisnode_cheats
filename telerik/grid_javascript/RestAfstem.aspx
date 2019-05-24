<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestAfstem.aspx.cs" Inherits="RestAfstem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Design/Styles.css" rel="stylesheet" />
    <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
   <div class="header" id="myHeader">
       <div>
           <a href="../index.aspx">Tilbage</a>
       </div>
        <div>
            Bank saldo: <label runat="server" id="lbl_banksaldo">0</label> kr. | ERP saldo: <label runat="server" id="lbl_erpsaldo">0</label> kr. | Kontroldifference (bør være 0): <label runat="server" id="lbl_saldoDifference">0</label> kr.
        </div>
        <div>
            Forskel på markerede: <label runat="server" id="result_label">0</label>
        </div>
        <div>
            <telerik:RadButton runat="server" ID="btnAfstem" OnClick="btnAfstem_Click" Text="Afstem valgte" /> <asp:Label runat="server" ID="lblErr" Text="" backcolor="red" />
        </div>
       <div>
           <telerik:RadTextBox runat="server" ID="txbComment" />
       </div>
    </div>
    <div class="content">
            <div class="divgrid">
                <h3>Bank</h3>
                <telerik:RadGrid runat="server" ID="Grid_Bank" RenderMode="Lightweight" ShowStatusBar="false" AutoGenerateColumns="false" AllowSorting="true"
                    AllowMultiRowEdit="false" AllowAutomaticUpdates="true" AllowPaging="false" OnPreRender="Grid_Bank_PreRender" AllowMultiRowSelection="true"
                    SelectableMode="ServerAndClientSide" AllowFilteringByColumn="true" GroupingSettings-CaseSensitive="false">
                    <MasterTableView AllowFilteringByColumn="true" DataKeyNames="system_id,amount" ClientDataKeyNames="system_id" AllowMultiColumnSorting="true">
                        <Columns>
                            <telerik:GridClientSelectColumn UniqueName="selected" />
                            <telerik:GridBoundColumn UniqueName="text" DataField="text" HeaderText="Tekst" FilterDelay="800" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn UniqueName="bogfoeringsdato" DataField="bogfoeringsdato" HeaderText="Bogføringsdato" DataType="System.DateTime" DataFormatString="{0:dd-MM-yyyy}" FilterDelay="800" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn UniqueName="amount" DataField="amount" HeaderText="Beløb" DataType="System.Decimal" DataFormatString="{0:###,##0.00}" FilterDelay="800" ShowFilterIcon="false" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true"></Selecting>
                        <ClientEvents OnRowCreated="rowCreatedBank" OnRowSelected="rowSelectedBank" OnRowDeselected="rowDeselectedBank" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
            <div class="divgrid">
                <h3>ERP</h3>
                <telerik:RadGrid runat = "server" ID="Grid_Erp" RenderMode="Lightweight" ShowStatusBar="false" AutoGenerateColumns="false" AllowSorting="true"
                AllowMultiRowEdit="false" AllowAutomaticUpdates="true" AllowPaging="false" OnPreRender="Grid_Erp_PreRender" AllowMultiRowSelection="true"
                SelectableMode="ServerAndClientSide" AllowFilteringByColumn="true" GroupingSettings-CaseSensitive="false">
                    <MasterTableView AllowFilteringByColumn = "true" DataKeyNames="system_id,amount" ClientDataKeyNames="system_id" AllowMultiColumnSorting="true">
                        <Columns>
                            <telerik:GridClientSelectColumn UniqueName = "selected" />
                            <telerik:GridBoundColumn UniqueName = "amount" DataField="amount" HeaderText="Beløb" DataType="System.Decimal" DataFormatString="{0:###,##0.00}"  FilterDelay="800" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn UniqueName = "bogfoeringsdato" DataField="bogfoeringsdato" HeaderText="Bogføringsdato" DataType="System.DateTime" DataFormatString="{0:dd-MM-yyyy}" FilterDelay="800" ShowFilterIcon="false"/>
                            <telerik:GridBoundColumn UniqueName = "text" DataField="text" HeaderText="Tekst" FilterDelay="800" ShowFilterIcon="false"/>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle = "true" >
                        <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true"></Selecting>
                        <ClientEvents OnRowCreated="rowCreatedErp" OnRowSelected="rowSelectedErp" OnRowDeselected="rowDeselectedErp" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
    </div>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="false">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="Grid_Bank">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Grid_Bank" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="Grid_Erp">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Grid_Erp" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            var selectedBank = {};
            function rowSelectedBank(sender, args) {
                //console.log(args);
                //console.log(args.getDataKeyValue("system_id"));
                var bankID = args.getDataKeyValue("system_id");
                if (!selectedBank[bankID]) {
                    selectedBank[bankID] = true;
                }
                RowSelectedCalc(sender, args);
            }
            function rowDeselectedBank(sender, args) {
                var bankID = args.getDataKeyValue("system_id");
                if (selectedBank[bankID]) {
                    selectedBank[bankID] = null;
                }
                RowSelectedCalc(sender, args);
            }
            function rowCreatedBank(sender, args) {
                var bankID = args.getDataKeyValue("system_id");
                if (selectedBank[bankID]) {
                    args.get_gridDataItem().set_selected(true);
                }
            }

            var selectedErp = {};
            function rowSelectedErp(sender, args) {
                var erpID = args.getDataKeyValue("system_id");
                if (!selectedErp[erpID]) {
                    selectedErp[erpID] = true;
                }
                RowSelectedCalc(sender, args);
            }
            function rowDeselectedErp(sender, args) {
                var erpID = args.getDataKeyValue("system_id");
                if (selectedErp[erpID]) {
                    selectedErp[erpID] = null;
                }
                RowSelectedCalc(sender, args);
            }
            function rowCreatedErp(sender, args) {
                var erpID = args.getDataKeyValue("system_id");
                if (selectedErp[erpID]) {
                    args.get_gridDataItem().set_selected(true);
                }
            }


        </script>
        <script type="text/javascript">
            function RowSelectedCalc(sender, eventArgs) {
                var e = eventArgs.get_domEvent();
                var result_label = document.getElementById("result_label");
                var bank_grid = $find("<%=Grid_Bank.ClientID %>");
                var erp_grid = $find("<%=Grid_Erp.ClientID %>");
                var bank_MasterTable = bank_grid.get_masterTableView();
                var erp_MasterTable = erp_grid.get_masterTableView();
                var bankSelectedRows = bank_MasterTable.get_selectedItems();
                var erpSelectedRows = erp_MasterTable.get_selectedItems();
                var bank_belob = 0;
                var erp_belob = 0
                var result = 0;

                var commentBox = document.getElementById('txbComment');
                if (bankSelectedRows.length === 1 && erpSelectedRows.length === 0) {
                    var b_r = bankSelectedRows[0];
                    var b_c = bank_MasterTable.getCellByColumnUniqueName(b_r, "amount")
                    commentBox.value = b_c.textContent;
                    console.log("bank");
                }
                else if (bankSelectedRows.length === 0 && erpSelectedRows.length === 1) {
                    var e_r = erpSelectedRows[j];
                    var e_c = erp_MasterTable.getCellByColumnUniqueName(e_r, "amount")
                    commentBox.value = e_c.textContent;
                    console.log("erp");
                }
                else {
                    commentBox.value = "";
                    console.log("intet");
                }


                //udregner samlede bank
                for (var i = 0; i < bankSelectedRows.length; i++) {
                    var bank_row = bankSelectedRows[i];
                    var bank_cell = bank_MasterTable.getCellByColumnUniqueName(bank_row, "amount")
                    //console.log(bank_cell.textContent);
                    var bank_btmp = bank_cell.textContent.split('.').join('');
                    //console.log(bank_btmp);
                    bank_belob = (parseFloat(bank_belob) + parseFloat(bank_btmp.replace(',','.'))).toFixed(2);
                }

                //udregner samlede erp
                for (var j = 0; j < erpSelectedRows.length; j++) {
                    var erp_row = erpSelectedRows[j];
                    var erp_cell = erp_MasterTable.getCellByColumnUniqueName(erp_row, "amount")
                    var erp_btmp = erp_cell.textContent.split('.').join('');
                    erp_belob = (parseFloat(erp_belob) + parseFloat(erp_btmp.replace(',', '.'))).toFixed(2);
                }

                result = bank_belob - erp_belob;
                result_label.textContent = result;

                if (result == 0) {
                    result_label.style.color = "black";
                }
                else
                {
                    result_label.style.color = "red";
                }
            };
        </script>
        <script type="text/javascript">
            // When the user scrolls the page, execute myFunction
            window.onscroll = function() {myFunction()};

            // Get the header
            var header = document.getElementById("myHeader");

            // Get the offset position of the navbar
            var sticky = header.offsetTop;

            // Add the sticky class to the header when you reach its scroll position. Remove "sticky" when you leave the scroll position
            function myFunction() {
                if (window.pageYOffset > sticky) {
                    header.classList.add("sticky");
                } else {
                    header.classList.remove("sticky");
                }
            }
        </script>
        </telerik:RadCodeBlock>

        

    </form>
</body>
</html>
