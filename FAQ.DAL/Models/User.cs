﻿using FAQ.SHARED.Enums;
using FAQ.DAL.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace FAQ.DAL.Models
{
    /// <summary>
    ///     A model class that represents the structure of the table User.
    ///     This model extents the IdentityUser by inheriting from <see cref="BaseIdentityInheritable"/> and all its properties.
    /// </summary>
    public class User : BaseIdentityInheritable
    {
        #region Properties

        /// <summary>
        ///     A <see cref="string"/> prop which default value is empty string, <see cref="string.Empty"/>.
        ///     This prop is configured to be required/not null using <see cref="RequiredAttribute"/>,
        ///     with a length of max 50 using using <see cref="StringLengthAttribute"/>.
        ///     Will hold the name of this user.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop which default value is empty string , <see cref="string.Empty"/>.
        ///     This prop is configured to be required/not null using <see cref="RequiredAttribute"/>, 
        ///     with a length of max 50 using using <see cref="StringLengthAttribute"/>. 
        ///     Will hold the Surname of this user.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Surname { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="Enum"/> prop which default value is Male.
        ///     Will hold the Gender of this user.
        /// </summary>
        [Required]
        public Gender Gender { get; set; } = SHARED.Enums.Gender.Male;
        /// <summary>
        ///     A <see cref="int"/> prop which default value is 0.
        ///     This prop is configured to be required/not null using <see cref="RequiredAttribute"/>, and with a range 
        ///     of none more less than 0 and more than 15 using <see cref="RangeAttribute"/>.
        ///     Will hold the Age of this user.
        /// </summary>
        [Required]
        [Range(maximum: 150, minimum: 0)]
        public int Age { get; set; } = 0;
        /// <summary>
        ///     A <see cref="string"/> prop which default value is empty string, <see cref="string.Empty"/>.
        ///     This prop is configured to be required/not null using <see cref="RequiredAttribute"/>,
        ///     with a length of max 100 using <see cref="StringLengthAttribute"/>. 
        ///     Will hold the Adress of this user.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Adress { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop which default value is empty string, <see cref="string.Empty"/>.
        ///     This prop is configured to be required/not null using <see cref="RequiredAttribute"/>,
        ///     with a length of max 50 using using <see cref="StringLengthAttribute"/>.
        ///     Will hold the Prefix of this user.
        /// </summary>
        [Required]
        [StringLength(4)]
        public string Prefix { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop which default value is empty string, <see cref="string.Empty"/>.
        ///     Will hold the OTP of this user.
        /// </summary>
        public string OTP { get; set; } = string.Empty;
        /// <summary>
        ///     A <see cref="string"/> prop which default value is empty string, <see cref="string.Empty"/>.
        ///     Will hold the ProfilePicture of this user.
        /// </summary>
        public string ProfilePicture { get;set; } = string.Empty;
        /// <summary>
        ///     A property used for inhertiance purposes with <see cref="Question"/> model and lazy loading.
        ///     Will hold the Questions of this user.
        ///     This property is nullable.
        /// </summary>
        public virtual ICollection<Question>? Questions { get; set; }
        /// <summary>
        ///     A property used for inhertiance purposes with <see cref="Answer"/> model and lazy loading.
        ///     Will hold the Answers of this user.
        ///     This property is nullable.
        /// </summary>
        public virtual ICollection<Answer>? Answers { get; set; }

        #endregion
    }
}