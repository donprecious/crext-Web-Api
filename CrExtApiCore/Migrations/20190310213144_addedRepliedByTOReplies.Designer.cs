﻿// <auto-generated />
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace CrExtApiCore.Migrations
{
    [DbContext(typeof(CrExtContext))]
    [Migration("20190310213144_addedRepliedByTOReplies")]
    partial class addedRepliedByTOReplies
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.AssignedProjects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignedByUserId");

                    b.Property<string>("AssignedToUserId");

                    b.Property<int>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("AssignedByUserId");

                    b.HasIndex("AssignedToUserId");

                    b.HasIndex("ProjectId");

                    b.ToTable("AssignedProjects");
                });

            modelBuilder.Entity("Entities.Customers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName");

                    b.Property<string>("Address");

                    b.Property<decimal>("BalanceIssued");

                    b.Property<string>("CustomData");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("Interest");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("NUBAN_NUMBER");

                    b.Property<string>("OtherName")
                        .HasMaxLength(50);

                    b.Property<decimal>("OutstandingBalance");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Recommendation");

                    b.Property<string>("Sex");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Entities.Organisations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BusinessAddress");

                    b.Property<string>("Description")
                        .HasMaxLength(400);

                    b.Property<string>("Name");

                    b.Property<string>("NatureOfBusiness");

                    b.Property<int>("PackageId");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("RCNumber");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("UserId");

                    b.ToTable("Organisations");
                });

            modelBuilder.Entity("Entities.PackagePRoles", b =>
                {
                    b.Property<int>("PackageId");

                    b.Property<int>("PRoleId");

                    b.HasKey("PackageId", "PRoleId");

                    b.HasIndex("PRoleId");

                    b.ToTable("PackagePRoles");
                });

            modelBuilder.Entity("Entities.Packages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(400);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Entities.Projects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(400);

                    b.Property<string>("Name");

                    b.Property<int>("OrganisationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Entities.PRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PRoles");
                });

            modelBuilder.Entity("Entities.Replies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Message");

                    b.Property<string>("Repliedby");

                    b.Property<int>("ReviewId");

                    b.Property<string>("status");

                    b.HasKey("Id");

                    b.HasIndex("Repliedby");

                    b.HasIndex("ReviewId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("Entities.ReviewActions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ReviewActions");
                });

            modelBuilder.Entity("Entities.ReviewKinds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ReviewKinds");
                });

            modelBuilder.Entity("Entities.ReviewNotifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("ReviewActionId");

                    b.Property<int>("ReviewId");

                    b.Property<int>("ReviewKindId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("ReviewActionId");

                    b.HasIndex("ReviewId");

                    b.HasIndex("ReviewKindId");

                    b.ToTable("ReviewNotifications");
                });

            modelBuilder.Entity("Entities.Reviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .HasMaxLength(1000);

                    b.Property<Guid>("CustomerId");

                    b.Property<int>("TeamMemberId");

                    b.Property<string>("status");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TeamMemberId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Entities.TeamMembers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("ProjectId");

                    b.Property<int>("TeamId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("TeamMembers");
                });

            modelBuilder.Entity("Entities.Teams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("OrganisationId");

                    b.Property<int>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Entities.UserOrganisation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrganisationId");

                    b.Property<int?>("OrganisationsId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationsId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOrganisations");
                });

            modelBuilder.Entity("Entities.Users", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Entities.Roles", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<string>("Description")
                        .HasMaxLength(400);

                    b.ToTable("Roles");

                    b.HasDiscriminator().HasValue("Roles");
                });

            modelBuilder.Entity("Entities.AssignedProjects", b =>
                {
                    b.HasOne("Entities.Users", "AssignedbyUser")
                        .WithMany()
                        .HasForeignKey("AssignedByUserId");

                    b.HasOne("Entities.Users", "AssignedToUser")
                        .WithMany()
                        .HasForeignKey("AssignedToUserId");

                    b.HasOne("Entities.Projects", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.Customers", b =>
                {
                    b.HasOne("Entities.Teams", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.Organisations", b =>
                {
                    b.HasOne("Entities.Packages", "Package")
                        .WithMany("Organisations")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Entities.PackagePRoles", b =>
                {
                    b.HasOne("Entities.PRole", "PRole")
                        .WithMany("PackagePRoles")
                        .HasForeignKey("PRoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Packages", "Package")
                        .WithMany("PackagePRoles")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.Projects", b =>
                {
                    b.HasOne("Entities.Organisations", "Organisation")
                        .WithMany("Projects")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.Replies", b =>
                {
                    b.HasOne("Entities.Users", "Users")
                        .WithMany()
                        .HasForeignKey("Repliedby");

                    b.HasOne("Entities.Reviews", "Review")
                        .WithMany()
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.ReviewNotifications", b =>
                {
                    b.HasOne("Entities.ReviewActions", "ReviewAction")
                        .WithMany("ReviewNotifications")
                        .HasForeignKey("ReviewActionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Reviews", "Review")
                        .WithMany()
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.ReviewKinds", "ReviewKind")
                        .WithMany("ReviewNotifications")
                        .HasForeignKey("ReviewKindId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.Reviews", b =>
                {
                    b.HasOne("Entities.Customers", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.TeamMembers", "TeamMember")
                        .WithMany()
                        .HasForeignKey("TeamMemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.TeamMembers", b =>
                {
                    b.HasOne("Entities.Projects", "Projects")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Teams", "Teams")
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Entities.Teams", b =>
                {
                    b.HasOne("Entities.Organisations", "organisation")
                        .WithMany("Teams")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Projects", "Project")
                        .WithMany("Teams")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.UserOrganisation", b =>
                {
                    b.HasOne("Entities.Organisations", "Organisations")
                        .WithMany()
                        .HasForeignKey("OrganisationsId");

                    b.HasOne("Entities.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entities.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entities.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Entities.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
