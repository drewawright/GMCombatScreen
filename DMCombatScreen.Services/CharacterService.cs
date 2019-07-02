﻿using DMCombatScreen.Data;
using DMCombatScreen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Services
{
    public class CharacterService
    {
        private readonly Guid _userID;

        public CharacterService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateCharacter(CharacterCreate model)
        {
            var entity =
                new Character()
                {
                    Name = model.Name,
                    IsPlayer = model.IsPlayer,
                    MaxHP = model.MaxHP,
                    InitiativeAbilityScore = model.InitiativeAbilityScore,
                    InitiativeModifier = model.InitiativeModifier,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Characters.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CharacterListItem> GetCharacters()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Characters
                        .Select(
                        e => new CharacterListItem
                        {
                            CharacterID = e.CharacterID,
                            Name = e.Name,
                            IsPlayer = e.IsPlayer,
                        }
                        );
                return query.ToArray();
            }
        }

        public CharacterDetail GetCharacterByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.
                        Characters.
                        Single(e => e.CharacterID == id);
                return
                    new CharacterDetail
                    {
                        CharacterID = entity.CharacterID,
                        Name = entity.Name,
                        MaxHP = entity.MaxHP,
                        InitiativeModifier = entity.InitiativeModifier,
                        InitiativeAbilityScore = entity.InitiativeAbilityScore,
                        IsPlayer = entity.IsPlayer
                    };
            }
        }

        public bool UpdateCharacter(CharacterEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Characters
                        .Single(e => e.CharacterID == model.CharacterID);

                entity.Name = model.Name;
                entity.MaxHP = model.MaxHP;
                entity.InitiativeModifier = model.InitiativeModifier;
                entity.InitiativeAbilityScore = model.InitiativeAbilityScore;
                entity.IsPlayer = model.IsPlayer;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCharacter(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Characters
                        .Single(e => e.CharacterID == characterID);
                ctx.Characters.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}