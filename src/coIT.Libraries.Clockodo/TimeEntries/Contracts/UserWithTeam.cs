namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    public class UserWithTeam : RawUser
    {
        public Team Team { get; set; }

        public static UserWithTeam FromRawUserAndTeam(RawUser u, Team team)
        {
            return new UserWithTeam(
                u.Id,
                u.Name,
                u.Number,
                u.Email,
                u.Role,
                u.Active,
                u.TimeFormat12h,
                u.WeekStartMonday,
                u.WeekEndFriday,
                u.Language,
                u.Timezone,
                u.WageType,
                u.CanGenerallySeeAbsences,
                u.CanGenerallyManageAbsences,
                u.CanAddCustomers,
                u.EditLock,
                u.EditLockDynamic,
                u.EditLockSync,
                u.WorktimeRegulationId,
                u.TeamId,
                u.NonBusinessGroups,
                u.Initials,
                u.Boss,
                u.AbsenceManagers,
                team
            );
        }

        public UserWithTeam(
            int id,
            string name,
            string? number,
            string email,
            string role,
            bool active,
            bool timeFormat12H,
            bool weekStartMonday,
            bool weekEndFriday,
            string language,
            string timezone,
            int? wageType,
            bool canGenerallySeeAbsences,
            bool canGenerallyManageAbsences,
            bool canAddCustomers,
            string? editLock,
            int? editLockDynamic,
            bool? editLockSync,
            int? worktimeRegulationId,
            int? teamId,
            int? nonBusinessGroups,
            string? initials,
            int? boss,
            IEnumerable<int> absenceManagers,
            Team team
        )
            : base(
                id,
                name,
                number,
                email,
                role,
                active,
                timeFormat12H,
                weekStartMonday,
                weekEndFriday,
                language,
                timezone,
                wageType,
                canGenerallySeeAbsences,
                canGenerallyManageAbsences,
                canAddCustomers,
                editLock,
                editLockDynamic,
                editLockSync,
                worktimeRegulationId,
                teamId,
                nonBusinessGroups,
                initials,
                boss,
                absenceManagers
            )
        {
            Team = team;
        }
    }
}
