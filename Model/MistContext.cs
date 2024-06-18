using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Mist.Model;

public partial class MistContext : DbContext
{
    public MistContext()
    {
    }

    public MistContext(DbContextOptions<MistContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameImage> GameImages { get; set; }

    public virtual DbSet<GameSysReq> GameSysReqs { get; set; }

    public virtual DbSet<GameVideo> GameVideos { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMember> GroupMembers { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGame> UserGames { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=mist", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.37-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("city");

            entity.HasIndex(e => e.CountryId, "fk_cities_country_id_idx");

            entity.HasIndex(e => e.StateId, "fk_cities_state_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("country_code")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.StateCode)
                .HasMaxLength(255)
                .HasColumnName("state_code")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.StateId).HasColumnName("state_id");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cities_country_id");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cities_state_id");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("country");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Currency)
                .HasMaxLength(255)
                .HasColumnName("currency")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(255)
                .HasColumnName("currency_name")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.CurrencySymbol)
                .HasMaxLength(255)
                .HasColumnName("currency_symbol")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Emoji)
                .HasMaxLength(191)
                .HasColumnName("emoji")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.EmojiU)
                .HasMaxLength(191)
                .HasColumnName("emojiU")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Iso2)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("iso2")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Iso3)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("iso3")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Nationality)
                .HasMaxLength(255)
                .HasColumnName("nationality")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Native)
                .HasMaxLength(255)
                .HasColumnName("native")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.NumericCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("numeric_code")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Phonecode)
                .HasMaxLength(255)
                .HasColumnName("phonecode")
                .UseCollation("utf8mb4_unicode_ci");
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("developer");

            entity.HasIndex(e => e.OwnerId, "fk_developer_group_owner_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BackgroundImage)
                .HasColumnType("mediumblob")
                .HasColumnName("background_image");
            entity.Property(e => e.Bio)
                .HasMaxLength(500)
                .HasColumnName("bio");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Pfp)
                .HasColumnType("mediumblob")
                .HasColumnName("pfp");

            entity.HasOne(d => d.Owner).WithMany(p => p.Developers)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_developer_group_owner_id");
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("friendship");

            entity.HasIndex(e => e.RecipientId, "fk_friend_recipient_id_idx");

            entity.HasIndex(e => e.SenderId, "fk_friend_sender_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FriendsSince)
                .HasColumnType("datetime")
                .HasColumnName("friends_since");
            entity.Property(e => e.Pending).HasColumnName("pending");
            entity.Property(e => e.RecipientId).HasColumnName("recipient_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");

            entity.HasOne(d => d.Recipient).WithMany(p => p.FriendshipRecipients)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_friend_recipient_id");

            entity.HasOne(d => d.Sender).WithMany(p => p.FriendshipSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_friend_sender_id");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("game");

            entity.HasIndex(e => e.DeveloperId, "fk_game_developer_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BackgroundLibraryImage)
                .HasColumnType("mediumblob")
                .HasColumnName("background_library_image");
            entity.Property(e => e.Bio)
                .HasMaxLength(500)
                .HasColumnName("bio");
            entity.Property(e => e.Description)
                .HasMaxLength(5000)
                .HasColumnName("description");
            entity.Property(e => e.DeveloperId).HasColumnName("developer_id");
            entity.Property(e => e.FrontImage)
                .HasColumnType("mediumblob")
                .HasColumnName("front_image");
            entity.Property(e => e.LibraryIcon)
                .HasColumnType("mediumblob")
                .HasColumnName("library_icon");
            entity.Property(e => e.LibraryLogo)
                .HasColumnType("mediumblob")
                .HasColumnName("library_logo");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.StoreSmallImage)
                .HasColumnType("mediumblob")
                .HasColumnName("store_small_image");
            entity.Property(e => e.UsdPrice)
                .HasPrecision(6, 2)
                .HasColumnName("usd_price");
            entity.Property(e => e.VerticalLibraryImage)
                .HasColumnType("mediumblob")
                .HasColumnName("vertical_library_image");

            entity.HasOne(d => d.Developer).WithMany(p => p.Games)
                .HasForeignKey(d => d.DeveloperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_developer_id");
        });

        modelBuilder.Entity<GameImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("game_image");

            entity.HasIndex(e => e.GameId, "fk_game_image_game_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Image)
                .HasColumnType("mediumblob")
                .HasColumnName("image");

            entity.HasOne(d => d.Game).WithMany(p => p.GameImages)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_image_game_id");
        });

        modelBuilder.Entity<GameSysReq>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("game_sys_req");

            entity.HasIndex(e => e.GameId, "fk_game_sys_req_game_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cpu)
                .HasMaxLength(200)
                .HasColumnName("cpu");
            entity.Property(e => e.Directx)
                .HasMaxLength(200)
                .HasColumnName("directx");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Gpu)
                .HasMaxLength(200)
                .HasColumnName("gpu");
            entity.Property(e => e.Memory)
                .HasMaxLength(200)
                .HasColumnName("memory");
            entity.Property(e => e.Os)
                .HasMaxLength(45)
                .HasColumnName("os");
            entity.Property(e => e.Storage)
                .HasMaxLength(200)
                .HasColumnName("storage");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .HasColumnName("type");

            entity.HasOne(d => d.Game).WithMany(p => p.GameSysReqs)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_sys_req_game_id");
        });

        modelBuilder.Entity<GameVideo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("game_video");

            entity.HasIndex(e => e.GameId, "fk_game_video_game_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Video).HasColumnName("video");

            entity.HasOne(d => d.Game).WithMany(p => p.GameVideos)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_video_game_id");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("group");

            entity.HasIndex(e => e.OwnerId, "fk_group_owner_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bio)
                .HasMaxLength(500)
                .HasColumnName("bio");
            entity.Property(e => e.IsPrivate).HasColumnName("is_private");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Pfp)
                .HasColumnType("mediumblob")
                .HasColumnName("pfp");
            entity.Property(e => e.Tag)
                .HasMaxLength(15)
                .HasColumnName("tag");

            entity.HasOne(d => d.Owner).WithMany(p => p.Groups)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_owner_id");
        });

        modelBuilder.Entity<GroupMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("group_member");

            entity.HasIndex(e => e.GroupId, "fk_group_member_group_id_idx");

            entity.HasIndex(e => e.MemberId, "fk_group_member_member_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.JoinDate)
                .HasColumnType("timestamp")
                .HasColumnName("join_date");
            entity.Property(e => e.MemberId).HasColumnName("member_id");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_member_group_id");

            entity.HasOne(d => d.Member).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_member_member_id");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("message");

            entity.HasIndex(e => e.RecipientId, "fk_message_recipient_id_idx");

            entity.HasIndex(e => e.SenderId, "fk_message_sender_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datetime)
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.Message1)
                .HasMaxLength(2500)
                .HasColumnName("message");
            entity.Property(e => e.RecipientId).HasColumnName("recipient_id");
            entity.Property(e => e.Seen).HasColumnName("seen");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");

            entity.HasOne(d => d.Recipient).WithMany(p => p.MessageRecipients)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_message_recipient_id");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_message_sender_id");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("review");

            entity.HasIndex(e => e.AuthorId, "fk_review_author_id_idx");

            entity.HasIndex(e => e.GameId, "fk_review_game_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Result).HasColumnName("result");
            entity.Property(e => e.Text)
                .HasMaxLength(2500)
                .HasColumnName("text");

            entity.HasOne(d => d.Author).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_review_author_id");

            entity.HasOne(d => d.Game).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_review_game_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("state");

            entity.HasIndex(e => e.CountryId, "fk_states_country_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("country_code")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.FipsCode)
                .HasMaxLength(255)
                .HasColumnName("fips_code")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Iso2)
                .HasMaxLength(255)
                .HasColumnName("iso2")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Type)
                .HasMaxLength(191)
                .HasColumnName("type")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_states_country_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.DisplayCityId, "fk_user_display_city_id_idx");

            entity.HasIndex(e => e.DisplayCountryId, "fk_user_display_country_id_idx");

            entity.HasIndex(e => e.DisplayStateId, "fk_user_display_state_id_idx");

            entity.HasIndex(e => e.RoleId, "fk_user_role_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Login, "login_UNIQUE").IsUnique();

            entity.HasIndex(e => e.CountryId, "zxc_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasPrecision(8, 2)
                .HasColumnName("balance");
            entity.Property(e => e.Bio)
                .HasMaxLength(500)
                .HasColumnName("bio");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.DisplayCityId).HasColumnName("display_city_id");
            entity.Property(e => e.DisplayCountryId).HasColumnName("display_country_id");
            entity.Property(e => e.DisplayStateId).HasColumnName("display_state_id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.Nickname)
                .HasMaxLength(25)
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.Pfp)
                .HasColumnType("mediumblob")
                .HasColumnName("pfp");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Country).WithMany(p => p.UserCountries)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_country_id");

            entity.HasOne(d => d.DisplayCity).WithMany(p => p.Users)
                .HasForeignKey(d => d.DisplayCityId)
                .HasConstraintName("fk_user_display_city_id");

            entity.HasOne(d => d.DisplayCountry).WithMany(p => p.UserDisplayCountries)
                .HasForeignKey(d => d.DisplayCountryId)
                .HasConstraintName("fk_user_display_country_id");

            entity.HasOne(d => d.DisplayState).WithMany(p => p.Users)
                .HasForeignKey(d => d.DisplayStateId)
                .HasConstraintName("fk_user_display_state_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role_id");
        });

        modelBuilder.Entity<UserGame>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_game");

            entity.HasIndex(e => e.GameId, "fk_user_game_game_id_idx");

            entity.HasIndex(e => e.UserId, "fk_user_game_user_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcquiredAt)
                .HasColumnType("datetime")
                .HasColumnName("acquired_at");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Game).WithMany(p => p.UserGames)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_game_game_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserGames)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_game_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
