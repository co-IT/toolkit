using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    public class RawUser
    {
        [JsonProperty("id")]
        public int Id { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("number")]
        public string? Number { get; init; }

        [JsonProperty("email")]
        public string Email { get; init; }

        [JsonProperty("role")]
        public string Role { get; init; }

        [JsonProperty("active")]
        public bool Active { get; init; }

        [JsonProperty("timeformat_12h")]
        public bool TimeFormat12h { get; init; }

        [JsonProperty("weekstart_monday")]
        public bool WeekStartMonday { get; init; }

        [JsonProperty("weekend_friday")]
        public bool WeekEndFriday { get; init; }

        [JsonProperty("language")]
        public string Language { get; init; }

        [JsonProperty("timezone")]
        public string Timezone { get; init; }

        [JsonProperty("wage_type")]
        public int? WageType { get; init; }

        [JsonProperty("can_generally_see_absences")]
        public bool CanGenerallySeeAbsences { get; init; }

        [JsonProperty("can_generally_manage_absences")]
        public bool CanGenerallyManageAbsences { get; init; }

        [JsonProperty("can_add_customers")]
        public bool CanAddCustomers { get; init; }

        [JsonProperty("edit_lock")]
        public string? EditLock { get; init; }

        [JsonProperty("edit_lock_dyn")]
        public int? EditLockDynamic { get; init; }

        [JsonProperty("edit_lock_sync")]
        public bool? EditLockSync { get; init; }

        [JsonProperty("worktime_regulation_id")]
        public int? WorktimeRegulationId { get; init; }

        [JsonProperty("teams_id")]
        public int? TeamId { get; init; }

        [JsonProperty("initials")]
        public string? Initials { get; init; }

        [JsonProperty("nonbusinessgroups_id")]
        public int? NonBusinessGroups { get; init; }

        [JsonProperty("boss")]
        public int? Boss { get; init; }

        [JsonProperty("absence_managers_id")]
        public IEnumerable<int> AbsenceManagers { get; init; }

        public RawUser(
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
            IEnumerable<int> absenceManagers
        )
        {
            Id = id;
            Name = name;
            Number = number;
            Email = email;
            Role = role;
            Active = active;
            TimeFormat12h = timeFormat12H;
            WeekStartMonday = weekStartMonday;
            WeekEndFriday = weekEndFriday;
            Language = language;
            Timezone = timezone;
            WageType = wageType;
            CanGenerallySeeAbsences = canGenerallySeeAbsences;
            CanGenerallyManageAbsences = canGenerallyManageAbsences;
            CanAddCustomers = canAddCustomers;
            EditLock = editLock;
            EditLockDynamic = editLockDynamic;
            EditLockSync = editLockSync;
            WorktimeRegulationId = worktimeRegulationId;
            TeamId = teamId;
            NonBusinessGroups = nonBusinessGroups;
            Initials = initials;
            Boss = boss;
            AbsenceManagers = absenceManagers;
        }
    }
}
