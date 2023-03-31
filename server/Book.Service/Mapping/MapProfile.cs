using AutoMapper;
using Book.Core.Dtos.Create;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.List;
using Book.Core.Dtos.Update;
using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ListItem, ListItemCreateDto>().ReverseMap();
            CreateMap<ListItem, ListItemShowDto>().ReverseMap();
            CreateMap<ListItem, ListItemUpdateDto>().ReverseMap();
            CreateMap<ListItem, ChecklistDto>().ReverseMap();
            CreateMap<Checklist, ChecklistDto>().ReverseMap();
            CreateMap<Checklist, ChecklistUpdateDto>().ReverseMap();
            CreateMap<Checklist, ChecklistShowDto>().ReverseMap();
            CreateMap<Checklist, ChecklistDto>().ReverseMap();
            CreateMap<Checklist, ChecklistWithItemsDto>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserShowDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<Organization, OrganizationDto>().ReverseMap();
            CreateMap<Organization, OrganizationShowDto>().ReverseMap();
            CreateMap<Organization, OrganizationUpdateDto>().ReverseMap();
            CreateMap<Subscription, SubDto>().ReverseMap();
            CreateMap<Subscription, SubCreateDto>().ReverseMap();
            CreateMap<Subscription, SubShowDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<Permission, PermissionUpdateDto>().ReverseMap();
            CreateMap<Permission, PermissionShowDto>().ReverseMap();
            CreateMap<PermissionUpdateDto, SubDto>().ReverseMap();
            CreateMap<BaseOption,BaseOptionDto>().ReverseMap();
            CreateMap<BaseOptionDto, Category>().ReverseMap();
            CreateMap<BaseOptionDto, Segment>().ReverseMap();
            CreateMap<BaseOptionDto, ControlList>().ReverseMap();
            CreateMap<BaseOptionDto, Consept>().ReverseMap();
            CreateMap<BaseOptionDto, Content>().ReverseMap();
            CreateMap<BaseOptionCreateDto, Category>().ReverseMap();
            CreateMap<BaseOptionCreateDto, Segment>().ReverseMap();
            CreateMap<BaseOptionCreateDto, ControlList>().ReverseMap();
            CreateMap<BaseOptionCreateDto, Consept>().ReverseMap();
            CreateMap<BaseOptionCreateDto, Content>().ReverseMap();
            CreateMap<BaseOptionUpdateDto, Category>().ReverseMap();
            CreateMap<BaseOptionUpdateDto, Segment>().ReverseMap();
            CreateMap<BaseOptionUpdateDto, ControlList>().ReverseMap();
            CreateMap<BaseOptionUpdateDto, Consept>().ReverseMap();
            CreateMap<BaseOptionUpdateDto, Content>().ReverseMap();
        }
    }
}
