namespace Cloud_Vibe.Areas.Administration.ViewModels.Comment
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using Cloud_Vibe.Data.Models;
    using Cloud_Vibe.Web.Infrastructure.Mapping;

    public class CommentViewModel : IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int? ID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "User")]
        public string UserShared { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [UIHint("Date")]
        [Display(Name = "Shared On")]
        public DateTime SharedOn { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Song")]
        public string SongName { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Album")]
        public string AlbumName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(m => m.UserShared, opt => opt.MapFrom(u => u.User.UserName));
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(m => m.SongName, opt => opt.MapFrom(u => u.Song.Title));
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(m => m.AlbumName, opt => opt.MapFrom(u => u.Album.Title));
        }
    }
}