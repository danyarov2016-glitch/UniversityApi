using AutoMapper;
using UniversityApi.DTO.Courses;
using UniversityApi.DTO.Enrollments;
using UniversityApi.DTO.Students;
using UniversityApi.DTO.Teachers;
using UniversityApi.Models;

namespace UniversityApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentDto>();
        CreateMap<StudentCreateDto, Student>();
        CreateMap<StudentUpdateDto, Student>();

        CreateMap<Teacher, TeacherDto>();
        CreateMap<TeacherCreateDto, Teacher>();
        CreateMap<TeacherUpdateDto, Teacher>();

        CreateMap<CourseCreateDto, Course>();
        CreateMap<CourseUpdateDto, Course>();
        CreateMap<Course, CourseDto>()
            .ForMember(d => d.TeacherName,
                o => o.MapFrom(s => s.Teacher == null ? string.Empty : s.Teacher.FirstName + " " + s.Teacher.LastName));

        CreateMap<EnrollmentCreateDto, Enrollment>();

        CreateMap<Enrollment, EnrollmentDto>()
            .ForMember(d => d.StudentName,
                o => o.MapFrom(s => s.Student == null ? string.Empty : s.Student.FirstName + " " + s.Student.LastName))
            .ForMember(d => d.CourseName,
                o => o.MapFrom(s => s.Course == null ? string.Empty : s.Course.Name));
    }
}
