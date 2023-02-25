﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class FetchDataToCountries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [dbo].[Countries] ON

                INSERT INTO [dbo].[Countries]
                           ([Id],
		                   [PhoneCode]
                           ,[CountryCode]
                           ,[CountryName], [IsDeleted])
                     VALUES
                            (1, 93, 'AF', 'Afghanistan', 0),
                            (2, 358, 'AX', 'Aland Islands', 0),
                            (3, 355, 'AL', 'Albania', 0),
                            (4, 213, 'DZ', 'Algeria', 0),
                            (5, 1684, 'AS', 'American Samoa', 0),
                            (6, 376, 'AD', 'Andorra', 0),
                            (7, 244, 'AO', 'Angola', 0),
                            (8, 1264, 'AI', 'Anguilla', 0),
                            (9, 672, 'AQ', 'Antarctica', 0),
                            (10, 1268, 'AG', 'Antigua and Barbuda', 0),
                            (11, 54, 'AR', 'Argentina', 0),
                            (12, 374, 'AM', 'Armenia', 0),
                            (13, 297, 'AW', 'Aruba', 0),
                            (14, 61, 'AU', 'Australia', 0),
                            (15, 43, 'AT', 'Austria', 0),
                            (16, 994, 'AZ', 'Azerbaijan', 0),
                            (17, 1242, 'BS', 'Bahamas', 0),
                            (18, 973, 'BH', 'Bahrain', 0),
                            (19, 880, 'BD', 'Bangladesh', 0),
                            (20, 1246, 'BB', 'Barbados', 0),
                            (21, 375, 'BY', 'Belarus', 0),
                            (22, 32, 'BE', 'Belgium', 0),
                            (23, 501, 'BZ', 'Belize', 0),
                            (24, 229, 'BJ', 'Benin', 0),
                            (25, 1441, 'BM', 'Bermuda', 0),
                            (26, 975, 'BT', 'Bhutan', 0),
                            (27, 591, 'BO', 'Bolivia', 0),
                            (28, 599, 'BQ', 'Bonaire, Sint Eustatius and Saba', 0),
                            (29, 387, 'BA', 'Bosnia and Herzegovina', 0),
                            (30, 267, 'BW', 'Botswana', 0),
                            (31, 55, 'BV', 'Bouvet Island', 0),
                            (32, 55, 'BR', 'Brazil', 0),
                            (33, 246, 'IO', 'British Indian Ocean Territory', 0),
                            (34, 673, 'BN', 'Brunei Darussalam', 0),
                            (35, 359, 'BG', 'Bulgaria', 0),
                            (36, 226, 'BF', 'Burkina Faso', 0),
                            (37, 257, 'BI', 'Burundi', 0),
                            (38, 855, 'KH', 'Cambodia', 0),
                            (39, 237, 'CM', 'Cameroon', 0),
                            (40, 1, 'CA', 'Canada', 0),
                            (41, 238, 'CV', 'Cape Verde', 0),
                            (42, 1345, 'KY', 'Cayman Islands', 0),
                            (43, 236, 'CF', 'Central African Republic', 0),
                            (44, 235, 'TD', 'Chad', 0),
                            (45, 56, 'CL', 'Chile', 0),
                            (46, 86, 'CN', 'China', 0),
                            (47, 61, 'CX', 'Christmas Island', 0),
                            (48, 672, 'CC', 'Cocos (Keeling) Islands', 0),
                            (49, 57, 'CO', 'Colombia', 0),
                            (50, 269, 'KM', 'Comoros', 0),
                            (51, 242, 'CG', 'Congo', 0),
                            (52, 242, 'CD', 'Congo, Democratic Republic of the Congo', 0),
                            (53, 682, 'CK', 'Cook Islands', 0),
                            (54, 506, 'CR', 'Costa Rica', 0),
                            (55, 225, 'CI', 'Cote Ivoire', 0),
                            (56, 385, 'HR', 'Croatia', 0),
                            (57, 53, 'CU', 'Cuba', 0),
                            (58, 599, 'CW', 'Curacao', 0),
                            (59, 357, 'CY', 'Cyprus', 0),
                            (60, 420, 'CZ', 'Czech Republic', 0),
                            (61, 45, 'DK', 'Denmark', 0),
                            (62, 253, 'DJ', 'Djibouti', 0),
                            (63, 1767, 'DM', 'Dominica', 0),
                            (64, 1809, 'DO', 'Dominican Republic', 0),
                            (65, 593, 'EC', 'Ecuador', 0),
                            (66, 20, 'EG', 'Egypt', 0),
                            (67, 503, 'SV', 'El Salvador', 0),
                            (68, 240, 'GQ', 'Equatorial Guinea', 0),
                            (69, 291, 'ER', 'Eritrea', 0),
                            (70, 372, 'EE', 'Estonia', 0),
                            (71, 251, 'ET', 'Ethiopia', 0),
                            (72, 500, 'FK', 'Falkland Islands (Malvinas)', 0),
                            (73, 298, 'FO', 'Faroe Islands', 0),
                            (74, 679, 'FJ', 'Fiji', 0),
                            (75, 358, 'FI', 'Finland', 0),
                            (76, 33, 'FR', 'France', 0),
                            (77, 594, 'GF', 'French Guiana', 0),
                            (78, 689, 'PF', 'French Polynesia', 0),
                            (79, 262, 'TF', 'French Southern Territories', 0),
                            (80, 241, 'GA', 'Gabon', 0),
                            (81, 220, 'GM', 'Gambia', 0),
                            (82, 995, 'GE', 'Georgia', 0),
                            (83, 49, 'DE', 'Germany', 0),
                            (84, 233, 'GH', 'Ghana', 0),
                            (85, 350, 'GI', 'Gibraltar', 0),
                            (86, 30, 'GR', 'Greece', 0),
                            (87, 299, 'GL', 'Greenland', 0),
                            (88, 1473, 'GD', 'Grenada', 0),
                            (89, 590, 'GP', 'Guadeloupe', 0),
                            (90, 1671, 'GU', 'Guam', 0),
                            (91, 502, 'GT', 'Guatemala', 0),
                            (92, 44, 'GG', 'Guernsey', 0),
                            (93, 224, 'GN', 'Guinea', 0),
                            (94, 245, 'GW', 'Guinea-Bissau', 0),
                            (95, 592, 'GY', 'Guyana', 0),
                            (96, 509, 'HT', 'Haiti', 0),
                            (97, 0, 'HM', 'Heard Island and Mcdonald Islands', 0),
                            (98, 39, 'VA', 'Holy See (Vatican City State)', 0),
                            (99, 504, 'HN', 'Honduras', 0),
                            (100, 852, 'HK', 'Hong Kong', 0),
                            (101, 36, 'HU', 'Hungary', 0),
                            (102, 354, 'IS', 'Iceland', 0),
                            (103, 91, 'IN', 'India', 0),
                            (104, 62, 'ID', 'Indonesia', 0),
                            (105, 98, 'IR', 'Iran, Islamic Republic of', 0),
                            (106, 964, 'IQ', 'Iraq', 0),
                            (107, 353, 'IE', 'Ireland', 0),
                            (108, 44, 'IM', 'Isle of Man', 0),
                            (109, 972, 'IL', 'Israel', 0),
                            (110, 39, 'IT', 'Italy', 0),
                            (111, 1876, 'JM', 'Jamaica', 0),
                            (112, 81, 'JP', 'Japan', 0),
                            (113, 44, 'JE', 'Jersey', 0),
                            (114, 962, 'JO', 'Jordan', 0),
                            (115, 7, 'KZ', 'Kazakhstan', 0),
                            (116, 254, 'KE', 'Kenya', 0),
                            (117, 686, 'KI', 'Kiribati', 0),
                            (118, 850, 'KP', 'Korea, Democratic People Republic of', 0),
                            (119, 82, 'KR', 'Korea, Republic of', 0),
                            (120, 381, 'XK', 'Kosovo', 0),
                            (121, 965, 'KW', 'Kuwait', 0),
                            (122, 996, 'KG', 'Kyrgyzstan', 0),
                            (123, 856, 'LA', 'Lao People Democratic Republic', 0),
                            (124, 371, 'LV', 'Latvia', 0),
                            (125, 961, 'LB', 'Lebanon', 0),
                            (126, 266, 'LS', 'Lesotho', 0),
                            (127, 231, 'LR', 'Liberia', 0),
                            (128, 218, 'LY', 'Libyan Arab Jamahiriya', 0),
                            (129, 423, 'LI', 'Liechtenstein', 0),
                            (130, 370, 'LT', 'Lithuania', 0),
                            (131, 352, 'LU', 'Luxembourg', 0),
                            (132, 853, 'MO', 'Macao', 0),
                            (133, 389, 'MK', 'Macedonia, the Former Yugoslav Republic of', 0),
                            (134, 261, 'MG', 'Madagascar', 0),
                            (135, 265, 'MW', 'Malawi', 0),
                            (136, 60, 'MY', 'Malaysia', 0),
                            (137, 960, 'MV', 'Maldives', 0),
                            (138, 223, 'ML', 'Mali', 0),
                            (139, 356, 'MT', 'Malta', 0),
                            (140, 692, 'MH', 'Marshall Islands', 0),
                            (141, 596, 'MQ', 'Martinique', 0),
                            (142, 222, 'MR', 'Mauritania', 0),
                            (143, 230, 'MU', 'Mauritius', 0),
                            (144, 269, 'YT', 'Mayotte', 0),
                            (145, 52, 'MX', 'Mexico', 0),
                            (146, 691, 'FM', 'Micronesia, Federated States of', 0),
                            (147, 373, 'MD', 'Moldova, Republic of', 0),
                            (148, 377, 'MC', 'Monaco', 0),
                            (149, 976, 'MN', 'Mongolia', 0),
                            (150, 382, 'ME', 'Montenegro', 0),
                            (151, 1664, 'MS', 'Montserrat', 0),
                            (152, 212, 'MA', 'Morocco', 0),
                            (153, 258, 'MZ', 'Mozambique', 0),
                            (154, 95, 'MM', 'Myanmar', 0),
                            (155, 264, 'NA', 'Namibia', 0),
                            (156, 674, 'NR', 'Nauru', 0),
                            (157, 977, 'NP', 'Nepal', 0),
                            (158, 31, 'NL', 'Netherlands', 0),
                            (159, 599, 'AN', 'Netherlands Antilles', 0),
                            (160, 687, 'NC', 'New Caledonia', 0),
                            (161, 64, 'NZ', 'New Zealand', 0),
                            (162, 505, 'NI', 'Nicaragua', 0),
                            (163, 227, 'NE', 'Niger', 0),
                            (164, 234, 'NG', 'Nigeria', 0),
                            (165, 683, 'NU', 'Niue', 0),
                            (166, 672, 'NF', 'Norfolk Island', 0),
                            (167, 1670, 'MP', 'Northern Mariana Islands', 0),
                            (168, 47, 'NO', 'Norway', 0),
                            (169, 968, 'OM', 'Oman', 0),
                            (170, 92, 'PK', 'Pakistan', 0),
                            (171, 680, 'PW', 'Palau', 0),
                            (172, 970, 'PS', 'Palestinian Territory, Occupied', 0),
                            (173, 507, 'PA', 'Panama', 0),
                            (174, 675, 'PG', 'Papua New Guinea', 0),
                            (175, 595, 'PY', 'Paraguay', 0),
                            (176, 51, 'PE', 'Peru', 0),
                            (177, 63, 'PH', 'Philippines', 0),
                            (178, 64, 'PN', 'Pitcairn', 0),
                            (179, 48, 'PL', 'Poland', 0),
                            (180, 351, 'PT', 'Portugal', 0),
                            (181, 1787, 'PR', 'Puerto Rico', 0),
                            (182, 974, 'QA', 'Qatar', 0),
                            (183, 262, 'RE', 'Reunion', 0),
                            (184, 40, 'RO', 'Romania', 0),
                            (185, 70, 'RU', 'Russian Federation', 0),
                            (186, 250, 'RW', 'Rwanda', 0),
                            (187, 590, 'BL', 'Saint Barthelemy', 0),
                            (188, 290, 'SH', 'Saint Helena', 0),
                            (189, 1869, 'KN', 'Saint Kitts and Nevis', 0),
                            (190, 1758, 'LC', 'Saint Lucia', 0),
                            (191, 590, 'MF', 'Saint Martin', 0),
                            (192, 508, 'PM', 'Saint Pierre and Miquelon', 0),
                            (193, 1784, 'VC', 'Saint Vincent and the Grenadines', 0),
                            (194, 684, 'WS', 'Samoa', 0),
                            (195, 378, 'SM', 'San Marino', 0),
                            (196, 239, 'ST', 'Sao Tome and Principe', 0),
                            (197, 966, 'SA', 'Saudi Arabia', 0),
                            (198, 221, 'SN', 'Senegal', 0),
                            (199, 381, 'RS', 'Serbia', 0),
                            (200, 381, 'CS', 'Serbia and Montenegro', 0),
                            (201, 248, 'SC', 'Seychelles', 0),
                            (202, 232, 'SL', 'Sierra Leone', 0),
                            (203, 65, 'SG', 'Singapore', 0),
                            (204, 1, 'SX', 'Sint Maarten', 0),
                            (205, 421, 'SK', 'Slovakia', 0),
                            (206, 386, 'SI', 'Slovenia', 0),
                            (207, 677, 'SB', 'Solomon Islands', 0),
                            (208, 252, 'SO', 'Somalia', 0),
                            (209, 27, 'ZA', 'South Africa', 0),
                            (210, 500, 'GS', 'South Georgia and the South Sandwich Islands', 0),
                            (211, 211, 'SS', 'South Sudan', 0),
                            (212, 34, 'ES', 'Spain', 0),
                            (213, 94, 'LK', 'Sri Lanka', 0),
                            (214, 249, 'SD', 'Sudan', 0),
                            (215, 597, 'SR', 'Suriname', 0),
                            (216, 47, 'SJ', 'Svalbard and Jan Mayen', 0),
                            (217, 268, 'SZ', 'Swaziland', 0),
                            (218, 46, 'SE', 'Sweden', 0),
                            (219, 41, 'CH', 'Switzerland', 0),
                            (220, 963, 'SY', 'Syrian Arab Republic', 0),
                            (221, 886, 'TW', 'Taiwan, Province of China', 0),
                            (222, 992, 'TJ', 'Tajikistan', 0),
                            (223, 255, 'TZ', 'Tanzania, United Republic of', 0),
                            (224, 66, 'TH', 'Thailand', 0),
                            (225, 670, 'TL', 'Timor-Leste', 0),
                            (226, 228, 'TG', 'Togo', 0),
                            (227, 690, 'TK', 'Tokelau', 0),
                            (228, 676, 'TO', 'Tonga', 0),
                            (229, 1868, 'TT', 'Trinidad and Tobago', 0),
                            (230, 216, 'TN', 'Tunisia', 0),
                            (231, 90, 'TR', 'Turkey', 0),
                            (232, 7370, 'TM', 'Turkmenistan', 0),
                            (233, 1649, 'TC', 'Turks and Caicos Islands', 0),
                            (234, 688, 'TV', 'Tuvalu', 0),
                            (235, 256, 'UG', 'Uganda', 0),
                            (236, 380, 'UA', 'Ukraine', 0),
                            (237, 971, 'AE', 'United Arab Emirates', 0),
                            (238, 44, 'GB', 'United Kingdom', 0),
                            (239, 1, 'US', 'United States', 0),
                            (240, 1, 'UM', 'United States Minor Outlying Islands', 0),
                            (241, 598, 'UY', 'Uruguay', 0),
                            (242, 998, 'UZ', 'Uzbekistan', 0),
                            (243, 678, 'VU', 'Vanuatu', 0),
                            (244, 58, 'VE', 'Venezuela', 0),
                            (245, 84, 'VN', 'Viet Nam', 0),
                            (246, 1284, 'VG', 'Virgin Islands, British', 0),
                            (247, 1340, 'VI', 'Virgin Islands, U.s.', 0),
                            (248, 681, 'WF', 'Wallis and Futuna', 0),
                            (249, 212, 'EH', 'Western Sahara', 0),
                            (250, 967, 'YE', 'Yemen', 0),
                            (251, 260, 'ZM', 'Zambia', 0),
                            (252, 263, 'ZW', 'Zimbabwe', 0);

                SET IDENTITY_INSERT [dbo].[Countries] OFF
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
