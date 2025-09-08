<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Portfolio.Admin.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard - Portfolio</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="../Styles/admin-dashboard.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <!-- Header -->
        <div class="header">
            <div class="header-content">
                <h1>Portfolio Admin Dashboard</h1>
                <div class="user-info">
                    Welcome, <strong><asp:Label ID="lblUsername" runat="server"></asp:Label></strong>
                    <a href="../Admin/Login.aspx?logout=true" class="logout-btn">Logout</a>
                </div>
            </div>
        </div>

        <!-- Main Container -->
        <div class="container">
            
            <!-- Message Panel -->
            <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                <div class="message">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
            </asp:Panel>

            <!-- Stats Cards -->
            <div class="stats-grid">
                <div class="stat-card">
                    <div class="stat-number"><asp:Label ID="lblSkillsCount" runat="server">0</asp:Label></div>
                    <div class="stat-label">Total Skills</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number"><asp:Label ID="lblProjectsCount" runat="server">0</asp:Label></div>
                    <div class="stat-label">Total Projects</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number">
                        <asp:Label ID="lblMessagesCount" runat="server">0</asp:Label>
                        <span class="unread-badge" id="unreadBadge" runat="server" visible="false">
                            <asp:Label ID="lblUnreadCount" runat="server"></asp:Label>
                        </span>
                    </div>
                    <div class="stat-label">Contact Messages</div>
                </div>
            </div>

            <!-- Navigation Tabs -->
            <div class="nav-tabs">
                <button type="button" class="nav-tab active" onclick="showSection('skills', this)">Manage Skills</button>
                <button type="button" class="nav-tab" onclick="showSection('projects', this)">Manage Projects</button>
                <button type="button" class="nav-tab" onclick="showSection('messages', this)">
                    Contact Messages
                    <span class="tab-badge" id="tabBadge" runat="server" visible="false">
                        <asp:Label ID="lblTabUnreadCount" runat="server"></asp:Label>
                    </span>
                </button>
            </div>

            <!-- Skills Section -->
            <div id="skills-section" class="content-section active">
                <div class="section-header">
                    <h2>Skills Management</h2>
                    <button type="button" class="add-btn" onclick="showAddSkillModal()">Add New Skill</button>
                </div>
                
                <asp:GridView ID="gvSkills" runat="server" CssClass="data-table" AutoGenerateColumns="false" 
                    OnRowCommand="gvSkills_RowCommand" DataKeyNames="id">
                    <Columns>
                        <asp:BoundField DataField="SkillName" HeaderText="Skill Name" />
                        <asp:BoundField DataField="Proficiency" HeaderText="Proficiency" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="action-container" data-id='<%# Eval("id") %>' 
                                     data-name='<%# Server.HtmlEncode(Eval("SkillName").ToString()) %>'
                                     data-proficiency='<%# Server.HtmlEncode(Eval("Proficiency").ToString()) %>'
                                     data-description='<%# Server.HtmlEncode(Eval("Description").ToString()) %>'>
                                    <button type="button" class="action-btn view-btn" 
                                        onclick="viewSkill(this)">View</button>
                                    <button type="button" class="action-btn edit-btn" 
                                        onclick="editSkill(this)">Edit</button>
                                    <asp:LinkButton runat="server" CssClass="action-btn delete-btn" 
                                        CommandName="DeleteSkill" CommandArgument='<%# Eval("id") %>' Text="Delete"
                                        OnClientClick="return confirm('Are you sure you want to delete this skill?');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Projects Section -->
            <div id="projects-section" class="content-section">
                <div class="section-header">
                    <h2>Projects Management</h2>
                    <button type="button" class="add-btn" onclick="showAddProjectModal()">Add New Project</button>
                </div>
                
                <asp:GridView ID="gvProjects" runat="server" CssClass="data-table" AutoGenerateColumns="false" 
                    OnRowCommand="gvProjects_RowCommand" DataKeyNames="id">
                    <Columns>
                        <asp:BoundField DataField="title" HeaderText="Title" />
                        <asp:BoundField DataField="technologies" HeaderText="Technologies" />
                        <asp:BoundField DataField="short_description" HeaderText="Description" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="action-container" data-id='<%# Eval("id") %>' 
                                     data-title='<%# Server.HtmlEncode(Eval("title").ToString()) %>'
                                     data-technologies='<%# Server.HtmlEncode(Eval("technologies").ToString()) %>'
                                     data-description='<%# Server.HtmlEncode(Eval("short_description").ToString()) %>'
                                     data-imagepath='<%# Server.HtmlEncode(Eval("image_path").ToString()) %>'
                                     data-websiteurl='<%# Server.HtmlEncode(Eval("website_url").ToString()) %>'
                                     data-githuburl='<%# Server.HtmlEncode(Eval("github_url").ToString()) %>'>
                                    <button type="button" class="action-btn view-btn" 
                                        onclick="viewProject(this)">View</button>
                                    <button type="button" class="action-btn edit-btn" 
                                        onclick="editProject(this)">Edit</button>
                                    <asp:LinkButton runat="server" CssClass="action-btn delete-btn" 
                                        CommandName="DeleteProject" CommandArgument='<%# Eval("id") %>' Text="Delete"
                                        OnClientClick="return confirm('Are you sure you want to delete this project?');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Contact Messages Section -->
            <div id="messages-section" class="content-section">
                <div class="section-header">
                    <h2>Contact Messages</h2>
                    <div class="message-filters">
                        <asp:Button ID="btnShowAll" runat="server" CssClass="filter-btn active" 
                            Text="All Messages" OnClick="btnShowAll_Click" />
                        <asp:Button ID="btnShowUnread" runat="server" CssClass="filter-btn" 
                            Text="Unread Only" OnClick="btnShowUnread_Click" />
                    </div>
                </div>
                
                <asp:GridView ID="gvMessages" runat="server" CssClass="data-table" AutoGenerateColumns="false" 
                    OnRowCommand="gvMessages_RowCommand" DataKeyNames="Id">
                    <Columns>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <span class='<%# Convert.ToBoolean(Eval("IsRead")) ? "status-read" : "status-unread" %>'>
                                    <%# Convert.ToBoolean(Eval("IsRead")) ? "Read" : "New" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="From" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy HH:mm}" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="action-container" data-id='<%# Eval("Id") %>' 
                                     data-name='<%# Server.HtmlEncode(Eval("Name").ToString()) %>'
                                     data-email='<%# Server.HtmlEncode(Eval("Email").ToString()) %>'
                                     data-subject='<%# Server.HtmlEncode(Eval("Subject").ToString()) %>'
                                     data-message='<%# Server.HtmlEncode(Eval("Message").ToString()) %>'
                                     data-date='<%# Eval("CreatedDate", "{0:MMM dd, yyyy HH:mm}") %>'
                                     data-isread='<%# Eval("IsRead") %>'>
                                    <button type="button" class="action-btn view-btn" 
                                        onclick="viewMessage(this)">View</button>
                                    <asp:LinkButton runat="server" CssClass="action-btn delete-btn" 
                                        CommandName="DeleteMessage" CommandArgument='<%# Eval("Id") %>' Text="Delete"
                                        OnClientClick="return confirm('Are you sure you want to delete this message?');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>

        <!-- View Skill Modal -->
        <div id="viewSkillModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeModal('viewSkillModal')">&times;</span>
                <h2>Skill Details</h2>
                
                <div class="form-group">
                    <label>Skill Name</label>
                    <div class="form-display" id="viewSkillName"></div>
                </div>
                
                <div class="form-group">
                    <label>Proficiency Level</label>
                    <div class="form-display" id="viewProficiency"></div>
                </div>
                
                <div class="form-group">
                    <label>Description</label>
                    <div class="form-display" id="viewSkillDescription"></div>
                </div>
                
                <div class="form-group">
                    <button type="button" class="action-btn" onclick="closeModal('viewSkillModal')">Close</button>
                </div>
            </div>
        </div>

        <!-- Add/Edit Skill Modal -->
        <div id="skillModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeModal('skillModal')">&times;</span>
                <h2 id="skillModalTitle">Add New Skill</h2>
                
                <asp:HiddenField ID="hfSkillId" runat="server" Value="0" />
                
                <div class="form-group">
                    <label>Skill Name</label>
                    <asp:TextBox ID="txtSkillName" runat="server" CssClass="form-control" placeholder="e.g., C#, JavaScript, SQL"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label>Proficiency Level</label>
                    <asp:DropDownList ID="ddlProficiency" runat="server" CssClass="form-control">
                        <asp:ListItem Value="">Select Proficiency</asp:ListItem>
                        <asp:ListItem Value="Beginner">Beginner</asp:ListItem>
                        <asp:ListItem Value="Intermediate">Intermediate</asp:ListItem>
                        <asp:ListItem Value="Advanced">Advanced</asp:ListItem>
                        <asp:ListItem Value="Expert">Expert</asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="form-group">
                    <label>Description</label>
                    <asp:TextBox ID="txtSkillDescription" runat="server" CssClass="form-control" 
                        TextMode="MultiLine" Rows="3" placeholder="Brief description of your experience with this skill"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <asp:Button ID="btnSaveSkill" runat="server" CssClass="add-btn" Text="Save Skill" OnClick="btnSaveSkill_Click" />
                    <button type="button" class="action-btn" onclick="closeModal('skillModal')">Cancel</button>
                </div>
            </div>
        </div>

        <!-- View Project Modal -->
        <div id="viewProjectModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeModal('viewProjectModal')">&times;</span>
                <h2>Project Details</h2>
                
                <div class="form-group">
                    <label>Project Title</label>
                    <div class="form-display" id="viewTitle"></div>
                </div>
                
                <div class="form-group">
                    <label>Technologies Used</label>
                    <div class="form-display" id="viewTechnologies"></div>
                </div>
                
                <div class="form-group">
                    <label>Description</label>
                    <div class="form-display" id="viewDescription"></div>
                </div>
                
                <div class="form-group">
                    <label>Image Path</label>
                    <div class="form-display" id="viewImagePath"></div>
                </div>
                
                <div class="form-group">
                    <label>Website URL</label>
                    <div class="form-display" id="viewWebsiteUrl"></div>
                </div>
                
                <div class="form-group">
                    <label>GitHub URL</label>
                    <div class="form-display" id="viewGithubUrl"></div>
                </div>
                
                <div class="form-group">
                    <button type="button" class="action-btn" onclick="closeModal('viewProjectModal')">Close</button>
                </div>
            </div>
        </div>

        <!-- Add/Edit Project Modal -->
        <div id="projectModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeModal('projectModal')">&times;</span>
                <h2 id="projectModalTitle">Add New Project</h2>
                
                <asp:HiddenField ID="hfProjectId" runat="server" Value="0" />
                
                <div class="form-group">
                    <label>Project Title</label>
                    <asp:TextBox ID="txtProjectTitle" runat="server" CssClass="form-control" placeholder="Project name"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label>Technologies Used</label>
                    <asp:TextBox ID="txtTechnologies" runat="server" CssClass="form-control" 
                        placeholder="e.g., ASP.NET, C#, SQL Server, HTML, CSS"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label>Short Description</label>
                    <asp:TextBox ID="txtProjectDescription" runat="server" CssClass="form-control" 
                        TextMode="MultiLine" Rows="3" placeholder="Brief project description"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label>Image Path</label>
                    <asp:TextBox ID="txtImagePath" runat="server" CssClass="form-control" 
                        placeholder="/images/project1.jpg (optional)"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label>Website URL</label>
                    <asp:TextBox ID="txtWebsiteUrl" runat="server" CssClass="form-control" 
                        placeholder="https://yourproject.com (optional)"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label>GitHub URL</label>
                    <asp:TextBox ID="txtGithubUrl" runat="server" CssClass="form-control" 
                        placeholder="https://github.com/username/repo (optional)"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <asp:Button ID="btnSaveProject" runat="server" CssClass="add-btn" Text="Save Project" OnClick="btnSaveProject_Click" />
                    <button type="button" class="action-btn" onclick="closeModal('projectModal')">Cancel</button>
                </div>
            </div>
        </div>

        <!-- View Message Modal -->
        <div id="viewMessageModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeModal('viewMessageModal')">&times;</span>
                <h2>Contact Message Details</h2>
                
                <div class="form-group">
                    <label>From</label>
                    <div class="form-display" id="viewMessageName"></div>
                </div>
                
                <div class="form-group">
                    <label>Email</label>
                    <div class="form-display" id="viewMessageEmail"></div>
                </div>
                
                <div class="form-group">
                    <label>Subject</label>
                    <div class="form-display" id="viewMessageSubject"></div>
                </div>
                
                <div class="form-group">
                    <label>Date</label>
                    <div class="form-display" id="viewMessageDate"></div>
                </div>
                
                <div class="form-group">
                    <label>Message</label>
                    <div class="form-display message-content" id="viewMessageContent"></div>
                </div>
                
                <div class="form-group">
                    <button type="button" class="action-btn" onclick="closeModal('viewMessageModal')">Close</button>
                    <a id="replyEmailLink" href="#" class="add-btn" target="_blank">Reply via Email</a>
                </div>
            </div>
        </div>

        <!-- Hidden field for tab persistence -->
        <asp:HiddenField ID="hfActiveTab" runat="server" Value="skills" />

    </form>

    <script src="../Scripts/dashboard.js"></script>
</body>
</html>

