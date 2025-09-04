<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Portfolio.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Home Section -->
    <section id="home" class="hero-section">
        <div class="container">
            <div class="hero-content">
                <div class="hero-text">
                    <h1 class="hero-title">Hi, I'm <span class="highlight">Your Name</span></h1>
                    <h2 class="hero-subtitle">Full Stack Web Developer</h2>
                    <p class="hero-description">
                        Computer Science student specializing in ASP.NET development and database-driven applications. 
                        Passionate about creating efficient, scalable web solutions.
                    </p>
                    <div class="hero-buttons">
                        <a href="#projects" class="btn btn-primary">View My Work</a>
                        <a href="#contact" class="btn btn-secondary">Contact</a>
                    </div>
                </div>
                <div class="hero-image">
                    <div class="profile-card">
                        <img src="Images/profile.jpg" alt="Profile Picture" class="profile-img" />
                        <div class="profile-glow"></div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- About Section -->
    <section id="about" class="about-section">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title">About Me</h2>
            </div>
            <div class="about-content">
                <p class="about-intro">
                    Hi, I'm a dedicated computer science student based in Bangladesh. I specialize in creating 
                    modern, responsive, and user-friendly web applications using ASP.NET, C#, JavaScript, 
                    and database technologies. I have strong problem-solving skills and enjoy tackling 
                    challenging programming tasks. I am passionate about learning new technologies and 
                    continuously improving my skills to deliver innovative and efficient solutions in web development.
                </p>
            </div>
        </div>
    </section>

    <!-- Education Section -->
    <section id="education" class="education-section">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title">Education</h2>
            </div>
            <div class="education-timeline">
                <div class="education-item">
                    <div class="education-year"></div>
                    <div class="education-degree">Bachelor of Science in Computer Science & Engineering</div>
                    <div class="education-school">Khulna University of engineering & Technology, Khulna</div>
                    <div class="education-details">
                        Focusing on software engineering, web development, and data structures. Doing projects on full-stack 
                        development, database design, and algorithms.
                    </div>
                </div>
                <div class="education-item">
                    <div class="education-year">2021</div>
                    <div class="education-degree">Higher Secondary Certificate (Science)</div>
                    <div class="education-school">Amla Government College, Mirpur, Kushtia</div>
                    <div class="education-details">
                        Jessore Board<br>
                        <span class="education-gpa">GPA: 5.00</span><br>
                    </div>
                </div>
                <div class="education-item">
                    <div class="education-year">2019</div>
                    <div class="education-degree">Secondary School Certificate (Science)</div>
                    <div class="education-school">Majihat High School, Mirpur, Kushtia</div>
                    <div class="education-details">
                        Jessore Board<br>
                        <span class="education-gpa">GPA: 4.44</span>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Skills Section -->
    <section id="skills" class="skills-section">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title">My Skills</h2>
            </div>
            <div class="skills-container">
                <div class="skill-category">
                    <h3>Frameworks</h3>
                    <asp:Repeater ID="rptFrameworkSkills" runat="server">
                        <ItemTemplate>
                            <div class="skill-item">
                                <div class="skill-header">
                                    <span class="skill-name"><%# Eval("SkillName") %></span>
                                    <span class="skill-percentage"><%# GetProficiencyPercentage(Eval("Proficiency").ToString()) %>%</span>
                                </div>
                                <div class="skill-progress">
                                    <div class="progress-fill" data-level="<%# GetProficiencyPercentage(Eval("Proficiency").ToString()) %>"></div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                
                <div class="skill-category">
                    <h3>Languages</h3>
                    <asp:Repeater ID="rptLanguageSkills" runat="server">
                        <ItemTemplate>
                            <div class="skill-item">
                                <div class="skill-header">
                                    <span class="skill-name"><%# Eval("SkillName") %></span>
                                    <span class="skill-percentage"><%# GetProficiencyPercentage(Eval("Proficiency").ToString()) %>%</span>
                                </div>
                                <div class="skill-progress">
                                    <div class="progress-fill" data-level="<%# GetProficiencyPercentage(Eval("Proficiency").ToString()) %>"></div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>

    <!-- Projects Section -->
    <section id="projects" class="projects-section">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title">My Projects</h2>
                <p class="section-subtitle">Some of my recent work</p>
            </div>
            <div class="projects-grid">
                <asp:Repeater ID="rptProjects" runat="server">
                    <ItemTemplate>
                        <div class="project-card">
                            <div class="project-image">
                                <img src='<%# !string.IsNullOrEmpty(Eval("image_path").ToString()) ? Eval("image_path") : "~/Images/default-project.jpg" %>' 
                                     alt='<%# Eval("title") %>' />
                                <div class="project-overlay">
                                    <div class="project-links">
                                        <%# !string.IsNullOrEmpty(Eval("website_url").ToString()) ? 
                                            "<a href='" + Eval("website_url") + "' target='_blank' class='project-link'>Live Demo</a>" : "" %>
                                        <%# !string.IsNullOrEmpty(Eval("github_url").ToString()) ? 
                                            "<a href='" + Eval("github_url") + "' target='_blank' class='project-link'>Source Code</a>" : "" %>
                                    </div>
                                </div>
                            </div>
                            <div class="project-content">
                                <h3 class="project-title"><%# Eval("title") %></h3>
                                <p class="project-description"><%# Eval("short_description") %></p>
                                <div class="project-tech">
                                    <%# FormatTechnologies(Eval("technologies").ToString()) %>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

    <!-- Contact Section -->
    <section id="contact" class="contact-section">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title">Contact Me</h2>
            </div>
            <div class="contact-content">
                <div class="contact-info">
                    <h3>GET IN TOUCH</h3>
                    <p>745 Kushtia-Meherpur Hwy</p>
                    <p>Mirpur-7030, Kushtia, Bangladesh</p>
                    <p>+880 1XXX-XXXXXX</p>
                    <p><a href="mailto:your.email@gmail.com">your.email@gmail.com</a></p>
                    
                    <div class="social-links">
                        <a href="https://linkedin.com/in/yourprofile" target="_blank">LinkedIn</a>
                        <a href="https://github.com/yourusername" target="_blank">GitHub</a>
                        <a href="https://facebook.com/yourprofile" target="_blank">Facebook</a>
                    </div>
                </div>
                <div class="contact-form">
                    <asp:Panel ID="pnlContactMessage" runat="server" Visible="false">
                        <div class="contact-message">
                            <asp:Label ID="lblContactMessage" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>
                    
                    <div class="form-group">
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-input" placeholder="Name"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtContactEmail" runat="server" CssClass="form-input" placeholder="Email"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtContactSubject" runat="server" CssClass="form-input" placeholder="Subject"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtContactMessage" runat="server" CssClass="form-input" 
                            TextMode="MultiLine" Rows="5" placeholder="Message"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnSendMessage" runat="server" CssClass="btn btn-primary" 
                            Text="SEND NOW" OnClick="btnSendMessage_Click" />
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>