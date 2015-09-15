using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTCT;
using CTCT.Components.Contacts;
using CTCT.Components.EmailCampaigns;
using System.Globalization;
using CTCT.Exceptions;
using System.Configuration;
using CTCT.Services;
using CTCT.Components.AccountService;
using CTCT.Components;


namespace ConstantContact
{
    public partial class _Default : System.Web.UI.Page
    {
        private IEmailCampaignService _emailCampaignService = null;
        private IContactService _contactService = null;
        private ICampaignScheduleService _emailCampaginScheduleService = null;
        private IListService _listService = null;
        private IAccountService _accountService = null;
        private string _apiKey = string.Empty;
        private string _accessToken = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
           


            if (Request.QueryString["code"] != null)
            {
                Label1.Visible = false;
                TextBox1.Visible = false;
                btnCreateCampaign.Visible = true;
                Button1.Visible = false;
            }
             //   PopulateCampaignTypeList();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            _apiKey = ConfigurationManager.AppSettings["APIKey"];
            Session["campaign"] = TextBox1.Text;
            try
            {
                string state = "ok";
                OAuth.AuthorizeFromWebApplication(HttpContext.Current
                  , state);


                //initialize ConstantContact members
                IUserServiceContext userServiceContext = new UserServiceContext(_accessToken, _apiKey);
                ConstantContactFactory _constantContactFactory = new ConstantContactFactory(userServiceContext);
                _emailCampaignService = _constantContactFactory.CreateEmailCampaignService();
                _emailCampaginScheduleService = _constantContactFactory.CreateCampaignScheduleService();
                _listService = _constantContactFactory.CreateListService();
                _accountService = _constantContactFactory.CreateAccountService();
            }
            catch (OAuth2Exception oauthEx)
            {
                //  MessageBox.Show(string.Format("Authentication failure: {0}", oauthEx.Message), "Warning");
            }


            //PopulateListOfCountries();
            //PopulateUSAndCanadaListOfStates();

            //GetListOfContacts();
            //PopulateEmailLists();

        }






        ResultSet<Contact> contactList = null;

    


        IList<ContactList> contactListFromListService = null;
        private string GetListService()
        {
            string _accessToken = OAuth.GetAccessTokenByCode(HttpContext.Current, Request.QueryString["code"].ToString());
            try
            {



                string _apiKey = ConfigurationManager.AppSettings["APIKey"];
                IUserServiceContext userServiceContext = new UserServiceContext(_accessToken, _apiKey);
                ConstantContactFactory _constantContactFactory = new ConstantContactFactory(userServiceContext);
                _listService = _constantContactFactory.CreateListService();
                //  EmailCampaign campaign = CreateCampaignFromInputs();

                List<ContactList> allcontacts = new List<ContactList>();
                contactListFromListService = _listService.GetLists(DateTime.Now.AddDays(-10));

                
            }
            catch (Exception ex)
            {

            }
            return _accessToken;
        }

 










        protected void btnCreateCampaign_Click(object sender, EventArgs e)
        {
            try
            {


                string _accessToken = GetListService();// OAuth.GetAccessTokenByCode(HttpContext.Current, Request.QueryString["code"].ToString());
                string _apiKey = ConfigurationManager.AppSettings["APIKey"];
                IUserServiceContext userServiceContext = new UserServiceContext(_accessToken, _apiKey);
                ConstantContactFactory _constantContactFactory = new ConstantContactFactory(userServiceContext);
                _emailCampaignService = _constantContactFactory.CreateEmailCampaignService();
                _emailCampaginScheduleService = _constantContactFactory.CreateCampaignScheduleService();
                //  EmailCampaign campaign = CreateCampaignFromInputs();
                EmailCampaign campaign = CreateCampaignFromInputs();
              
                var savedCampaign = _emailCampaignService.AddCampaign(campaign);

                if (savedCampaign != null)
                {
                    //campaign was saved, but need to schedule it, if the case
                    Schedule schedule = null;


                    {
                        schedule = new Schedule() { ScheduledDate = Convert.ToDateTime(DateTime.Now.AddMinutes(1).ToShortTimeString().Trim()).ToUniversalTime() };
                    }

                    Schedule savedSchedule = _emailCampaginScheduleService.AddSchedule(savedCampaign.Id, schedule);

                   
                }

            }
            catch (Exception ex)
            {

            }

        }



        private EmailCampaign CreateCampaignFromInputs()
        {
            
            EmailCampaign campaign = new EmailCampaign();

            #region General settings

            // if (!string.IsNullOrWhiteSpace(txtCampaignName.Text))
            {
                campaign.Name ="Campaign Name here";
            }

            //  if (cbCampaignType.SelectedItem != null)
            {
                //campaign.TemplateType = GetCampaignType(cbCampaignType.SelectedItem as ItemInfo);
            }

            //   if (!string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                campaign.Subject = "subject here";
            }

            //   if (!string.IsNullOrWhiteSpace(txtFromName.Text))
            {
                campaign.FromName = "FromName";
            }

            //   if (cbFromEmail.SelectedIndex != null)
            {
                campaign.FromEmail = "FromEmail"; 
            }

            //  if (cbReplyToEmail.SelectedIndex != null)
            {
                campaign.ReplyToEmail = "ReplyEmail";
            }

            //   if (rbnHtml.Checked)
            {
                campaign.EmailContentFormat = CampaignEmailFormat.HTML;
            }
            // else
            // {
            //     campaign.EmailContentFormat = CampaignEmailFormat.XHTML;
            //   }

            //    if (!string.IsNullOrWhiteSpace(txtGreetingString.Text))
            {
                campaign.GreetingString = "greetings";
            }

            //      if (!string.IsNullOrWhiteSpace(txtEmailContent.Text))
            {

                campaign.EmailContent = "email body";
              
            }

            //   if (!string.IsNullOrWhiteSpace(txtContent.Text))
            {
                campaign.TextContent = ".text content";
            }

            #endregion General settings

            //#region Message footer settings

            ////organization name is required for message footer section
            //if (!string.IsNullOrWhiteSpace(txtOrganizationName.Text))
            //{
            MessageFooter msgFooter = new MessageFooter();

            //    if (!string.IsNullOrWhiteSpace(txtOrganizationName.Text))
            //    {
            msgFooter.OrganizationName = "Org name";
            //    }

            //    if (!string.IsNullOrWhiteSpace(txtAddressLine1.Text))
            //    {
            msgFooter.AddressLine1 = "address";
            //    }

            //    if (!string.IsNullOrWhiteSpace(txtAddressLine2.Text))
            //    {
            //        msgFooter.AddressLine2 = txtAddressLine2.Text.Trim();
            //    }

            //    if (!string.IsNullOrWhiteSpace(txtAddressLine3.Text))
            //    {
            //        msgFooter.AddressLine3 = txtAddressLine3.Text.Trim();
            //    }

            //    if (!string.IsNullOrWhiteSpace(txtCity.Text))
            //    {
            msgFooter.City = "NJ";
            //    }

            //    if (!string.IsNullOrWhiteSpace(txtZip.Text))
            //    {
            msgFooter.PostalCode = "07095";
            //    }

            //    if (cbCountry.SelectedItem != null)
            //    {
            msgFooter.Country = "US";// GetSelectedValue(cbCountry.SelectedItem as ItemInfo);
            //    }
            //    if (cbState.SelectedItem != null)
            //    {
            //        var state = cbState.SelectedItem as ItemInfo;
            //        if (state != null)
            //        {
            msgFooter.State = "NJ";
            //        }
            //    }


            //    if (chkIncludeForwardEmail.Checked)
            //    {
            //        msgFooter.ForwardEmailLinkText = txtIncludeForwardEmail.Text.Trim();
            //        msgFooter.IncludeForwardEmail = true;
            //    }

            //    if (chkIncludeSubscribeLink.Checked)
            //    {
            //        msgFooter.SubscribeLinkText = txtIncludeSubscribeLink.Text.Trim();
            //   msgFooter.IncludeSubscribeLink = true;
            //    }

            campaign.MessageFooter = msgFooter;
            //}

            //#endregion Message footer settings

            #region Contact lists settings

            List<SentContactList> lists = new List<SentContactList>();

            if (contactListFromListService.Count > 0)
            {
                foreach (var item in contactListFromListService.ToList<ContactList>())
                {
                    SentContactList contactList = new SentContactList()
                    {
                        Id = item.Id
                    };

                    lists.Add(contactList);
                }
            }



            campaign.Lists = lists;

            #endregion Contact lists settings

            #region Schedule campaign settings

            //if (rbnDraft.Checked)
            //{
            //    campaign.Status = CampaignStatus.DRAFT;
            //}
            //else if (rbnSendNow.Checked)
            //{
            //  campaign.Status = CampaignStatus.SENT;
            //}
            //else if (rbnScheduled.Checked)
            //{
            campaign.Status = CampaignStatus.SCHEDULED;
            //}

            #endregion Schedule campaign settings
          
            return campaign;
        }











    }
}