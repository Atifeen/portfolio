using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portfolio.Helpers;

namespace Portfolio.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if admin is logged in
            if (Session["AdminUsername"] == null)
            {
                Response.Redirect("~/Admin/Login.aspx");
                return;
            }

            // Handle logout
            if (Request.QueryString["logout"] == "true")
            {
                Session.Clear();
                Response.Redirect("~/Admin/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            try
            {
                // Set username
                lblUsername.Text = Session["AdminUsername"].ToString();

                // Load statistics
                LoadStats();

                // Load data grids
                LoadSkillsGrid();
                LoadProjectsGrid();
                LoadMessagesGrid();

                // Hide message panel
                pnlMessage.Visible = false;
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading dashboard: " + ex.Message, false);
            }
        }

        private void LoadStats()
        {
            try
            {
                lblSkillsCount.Text = DBHelper.GetSkillsCount().ToString();
                lblProjectsCount.Text = DBHelper.GetProjectsCount().ToString();
                
                // Load contact messages stats
                int totalMessages = DBHelper.GetContactMessagesCount();
                int unreadCount = DBHelper.GetUnreadMessagesCount();
                
                lblMessagesCount.Text = totalMessages.ToString();
                
                // Show unread badges if there are unread messages
                if (unreadCount > 0)
                {
                    unreadBadge.Visible = true;
                    tabBadge.Visible = true;
                    lblUnreadCount.Text = unreadCount.ToString();
                    lblTabUnreadCount.Text = unreadCount.ToString();
                }
                else
                {
                    unreadBadge.Visible = false;
                    tabBadge.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading statistics: " + ex.Message, false);
            }
        }

        private void LoadSkillsGrid()
        {
            try
            {
                DataTable dt = DBHelper.GetAllSkills();
                gvSkills.DataSource = dt;
                gvSkills.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading skills: " + ex.Message, false);
            }
        }

        private void LoadProjectsGrid()
        {
            try
            {
                DataTable dt = DBHelper.GetAllProjects();
                gvProjects.DataSource = dt;
                gvProjects.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading projects: " + ex.Message, false);
            }
        }

        private void LoadMessagesGrid()
        {
            try
            {
                DataTable dt = DBHelper.GetAllContactMessages();
                gvMessages.DataSource = dt;
                gvMessages.DataBind();
                
                // Set filter button states
                btnShowAll.CssClass = "filter-btn active";
                btnShowUnread.CssClass = "filter-btn";
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading messages: " + ex.Message, false);
            }
        }

        private void LoadUnreadMessagesGrid()
        {
            try
            {
                DataTable dt = DBHelper.GetUnreadMessages();
                gvMessages.DataSource = dt;
                gvMessages.DataBind();
                
                // Set filter button states
                btnShowAll.CssClass = "filter-btn";
                btnShowUnread.CssClass = "filter-btn active";
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading unread messages: " + ex.Message, false);
            }
        }

        // Skills GridView Events
        protected void gvSkills_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int skillId = Convert.ToInt32(e.CommandArgument);

            // Set active tab to skills when performing skill operations
            hfActiveTab.Value = "skills";

            try
            {
                if (e.CommandName == "DeleteSkill")
                {
                    DeleteSkill(skillId);
                }
                // Note: ViewSkill and EditSkill are now handled client-side
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, false);
            }
        }

        // Projects GridView Events
        protected void gvProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int projectId = Convert.ToInt32(e.CommandArgument);

            // Set active tab to projects when performing project operations
            hfActiveTab.Value = "projects";

            try
            {
                if (e.CommandName == "DeleteProject")
                {
                    DeleteProject(projectId);
                }
                // Note: ViewProject and EditProject are now handled client-side
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, false);
            }
        }

        // Messages GridView Events
        protected void gvMessages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int messageId = Convert.ToInt32(e.CommandArgument);

            // Set active tab to messages when performing message operations
            hfActiveTab.Value = "messages";

            try
            {
                if (e.CommandName == "DeleteMessage")
                {
                    DeleteMessage(messageId);
                }
                // Note: ViewMessage is handled client-side but marks as read
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, false);
            }
        }

        // Message Filter Events
        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            hfActiveTab.Value = "messages";
            LoadMessagesGrid();
        }

        protected void btnShowUnread_Click(object sender, EventArgs e)
        {
            hfActiveTab.Value = "messages";
            LoadUnreadMessagesGrid();
        }

        // Skill Operations
        private void DeleteSkill(int skillId)
        {
            try
            {
                bool success = DBHelper.DeleteSkill(skillId);
                if (success)
                {
                    ShowMessage("Skill deleted successfully!", true);
                    LoadSkillsGrid();
                    LoadStats();
                }
                else
                {
                    ShowMessage("Failed to delete skill.", false);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error deleting skill: " + ex.Message, false);
            }
        }

        protected void btnSaveSkill_Click(object sender, EventArgs e)
        {
            // Keep on skills tab
            hfActiveTab.Value = "skills";

            try
            {
                string skillName = txtSkillName.Text.Trim();
                string proficiency = ddlProficiency.SelectedValue;
                string description = txtSkillDescription.Text.Trim();

                // Basic validation
                if (string.IsNullOrEmpty(skillName) || string.IsNullOrEmpty(proficiency))
                {
                    ShowMessage("Please fill in all required fields.", false);
                    return;
                }

                bool success = false;
                int skillId = Convert.ToInt32(hfSkillId.Value);

                if (skillId == 0)
                {
                    // Add new skill
                    success = DBHelper.AddSkill(skillName, proficiency, description);
                    if (success)
                    {
                        ShowMessage("Skill added successfully!", true);
                    }
                }
                else
                {
                    // Update existing skill
                    success = DBHelper.UpdateSkill(skillId, skillName, proficiency, description);
                    if (success)
                    {
                        ShowMessage("Skill updated successfully!", true);
                    }
                }

                if (success)
                {
                    LoadSkillsGrid();
                    LoadStats();

                    // Hide modal and clear form
                    string script = @"
                        document.getElementById('skillModal').style.display = 'none';
                        clearSkillForm();
                    ";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideSkillModal", script, true);
                }
                else
                {
                    ShowMessage("Failed to save skill.", false);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error saving skill: " + ex.Message, false);
            }
        }

        // Project Operations
        private void DeleteProject(int projectId)
        {
            try
            {
                bool success = DBHelper.DeleteProject(projectId);
                if (success)
                {
                    ShowMessage("Project deleted successfully!", true);
                    LoadProjectsGrid();
                    LoadStats();
                }
                else
                {
                    ShowMessage("Failed to delete project.", false);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error deleting project: " + ex.Message, false);
            }
        }

        protected void btnSaveProject_Click(object sender, EventArgs e)
        {
            // Keep on projects tab
            hfActiveTab.Value = "projects";

            try
            {
                string title = txtProjectTitle.Text.Trim();
                string technologies = txtTechnologies.Text.Trim();
                string description = txtProjectDescription.Text.Trim();
                string imagePath = txtImagePath.Text.Trim();
                string websiteUrl = txtWebsiteUrl.Text.Trim();
                string githubUrl = txtGithubUrl.Text.Trim();

                // Basic validation
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(technologies))
                {
                    ShowMessage("Please fill in required fields (Title and Technologies).", false);
                    return;
                }

                bool success = false;
                int projectId = Convert.ToInt32(hfProjectId.Value);

                if (projectId == 0)
                {
                    // Add new project
                    success = DBHelper.AddProject(title, description, technologies, imagePath, websiteUrl, githubUrl);
                    if (success)
                    {
                        ShowMessage("Project added successfully!", true);
                    }
                }
                else
                {
                    // Update existing project
                    success = DBHelper.UpdateProject(projectId, title, description, technologies, imagePath, websiteUrl, githubUrl);
                    if (success)
                    {
                        ShowMessage("Project updated successfully!", true);
                    }
                }

                if (success)
                {
                    LoadProjectsGrid();
                    LoadStats();

                    // Hide modal and clear form
                    string script = @"
                        document.getElementById('projectModal').style.display = 'none';
                        clearProjectForm();
                    ";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideProjectModal", script, true);
                }
                else
                {
                    ShowMessage("Failed to save project.", false);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error saving project: " + ex.Message, false);
            }
        }

        // Message Operations
        private void DeleteMessage(int messageId)
        {
            try
            {
                bool success = DBHelper.DeleteContactMessage(messageId);
                if (success)
                {
                    ShowMessage("Message deleted successfully!", true);
                    LoadMessagesGrid();
                    LoadStats();
                }
                else
                {
                    ShowMessage("Failed to delete message.", false);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error deleting message: " + ex.Message, false);
            }
        }

        // Web Method for AJAX calls to mark message as read
        [System.Web.Services.WebMethod]
        public static bool MarkMessageAsRead(int messageId)
        {
            try
            {
                return DBHelper.MarkMessageAsRead(messageId);
            }
            catch
            {
                return false;
            }
        }

        // Helper method to display messages
        private void ShowMessage(string message, bool isSuccess)
        {
            pnlMessage.Visible = true;
            lblMessage.Text = message;
            if (isSuccess)
            {
                lblMessage.CssClass = "message success";
            }
            else
            {
                lblMessage.CssClass = "message error";
            }
        }
    }
}