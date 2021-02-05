using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using Avalon.Core.Phases;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    public class TeamBuildingPhase : IPhase
    {
        public TeamBuildingPhase(
            IAvalonContext context,
            int round,
            int unsuccessfullVotes)
        {
            Context = context;
            Round = round;
            UnsuccessfullVotes = unsuccessfullVotes;
        }

        int Round { get; set; }
        public int UnsuccessfullVotes { get; }

        Dictionary<int, Dictionary<int, int>> teamSizesTable = new Dictionary<int, Dictionary<int, int>>()
        {
            { 5, new Dictionary<int, int>() { { 1, 2 }, { 2, 3}, { 3, 2}, { 4, 3}, { 5, 3} } },
            { 6, new Dictionary<int, int>() { { 1, 2 }, { 2, 3}, { 3, 4}, { 4, 3}, { 5, 4} } },
            { 7, new Dictionary<int, int>() { { 1, 2 }, { 2, 3}, { 3, 3}, { 4, 4}, { 5, 4} } },
            { 8, new Dictionary<int, int>() { { 1, 3 }, { 2, 4}, { 3, 4}, { 4, 5}, { 5, 5} } },
            { 9, new Dictionary<int, int>() { { 1, 3 }, { 2, 4}, { 3, 4}, { 4, 5}, { 5, 5} } },
            { 10, new Dictionary<int, int>() { { 1, 3 }, { 2, 4}, { 3, 4}, { 4, 5}, { 5, 5} } },

        };

        public IAvalonContext Context { get; }

        public async Task<IPhase> Execute()
        {
            var teamSize = GetTeamSize(Context.Users.Count(), Round);
            await Context.Leader.SendMessage(string.Format("Please assign {0} players to quest team", teamSize));
            var team = await Context.Leader.SelectUsers(teamSize);

            await Context.SendMessage(string.Format("Proposed team is {0}", string.Join(",", team.Select(user => user.Name))));
            TransferLeadership();

            await Context.SendMessage(string.Format("New leader is {0}", Context.Leader.Name));

            var votes = await Task.WhenAll(Context.Users.Select(x => x.SelectVoteToken()));
            if (votes.Count(x => x == VoteToken.VoteApproved) > votes.Count(x => x == VoteToken.VoteRejected))
            {
                await Context.SendMessage("Team is approved");

                new QuestPhase(Context, team, Round);
            }
            else
            {
                if (UnsuccessfullVotes == 5)
                {
                    await Context.SendMessage("Uncessessfull votes limit reached");

                    new EvilWonPhase(Context);
                }
                else
                {
                    await Context.SendMessage(string.Format("Team not approved, attempts {0}", UnsuccessfullVotes + 1));

                    new TeamBuildingPhase(Context, Round, UnsuccessfullVotes + 1);
                }
            }

            return null;
        }

        private void TransferLeadership()
        {
            var leaderIndex = Context.Users.IndexOf(Context.Leader);
            Context.Leader = Context.Users.ElementAt(leaderIndex == Context.Users.Count - 1 ? 0 : leaderIndex + 1);
        }

        public int GetTeamSize(int playersCount, int round)
        {
            return teamSizesTable[playersCount][round];
        }
    }
}
