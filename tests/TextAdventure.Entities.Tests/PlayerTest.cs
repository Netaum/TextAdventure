using System.Collections.Generic;
using Moq;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;
using TextAdventure.Interfaces.Scenes;
using IInteraction = TextAdventure.Interfaces.IInteraction;
using IResponseAction = TextAdventure.Interfaces.IResponseAction;
using Xunit;

namespace TextAdventure.Entities.Tests
{
	public class PlayerTest
	{
		private Player player;
		private Mock<IGameController> controllerMock;
		private Mock<IScene> sceneMock;

		public PlayerTest()
		{
			controllerMock = new Mock<IGameController>();
			sceneMock = new Mock<IScene>();

			player = new Player(controllerMock.Object, true);
		}

		[Fact]
		public void EnterScene_ShouldCall()
		{
			player.EnterScene(sceneMock.Object);
			Assert.Equal(player.CurrentScene, sceneMock.Object);
		}

		[Fact]
		public void Inventory_HasItem_EmptyList_ShouldReturnFalse()
		{
			var resp = player.HasItem("test item");
			Assert.False(resp);
		}

		[Fact]
		public void Inventory_HasItem_WithItens_ShouldReturnFalse()
		{
			var item = new Mock<IInteractableObject>();
			item.Setup(s => s.Name)
				.Returns("Item 01");

			player.Inventory.Add(item.Object);

			var resp = player.HasItem("test item");
			Assert.False(resp);
		}

		[Fact]
		public void Inventory_HasItem_WithItens_ShouldReturnTrue()
		{
			var item = new Mock<IInteractableObject>();
			item.Setup(s => s.Name)
				.Returns("Item 01");

			player.Inventory.Add(item.Object);

			var resp = player.HasItem("Item 01");
			Assert.True(resp);
		}

		[Fact]
		public void AddItem_ShouldAdd()
		{
			var item = new Mock<IInteractableObject>();
			item.Setup(s => s.Name)
				.Returns("Item 01");

			player.AddItem(item.Object);
			Assert.Single(player.Inventory);
		}

		[Fact]
		public void AddItem_Duplicate_ShouldAddOnlyOne()
		{
			var item1 = new Mock<IInteractableObject>();
			item1.Setup(s => s.Name)
				.Returns("Item 01");

			var item2 = new Mock<IInteractableObject>();
			item2.Setup(s => s.Name)
				.Returns("Item 01");

			player.AddItem(item1.Object);
			player.AddItem(item2.Object);
			Assert.Single(player.Inventory);
		}

		[Fact]
		public void AddItem_MoreThanOne_ShouldAdd()
		{
			var item1 = new Mock<IInteractableObject>();
			item1.Setup(s => s.Name)
				.Returns("Item 01");

			var item2 = new Mock<IInteractableObject>();
			item2.Setup(s => s.Name)
				.Returns("Item 02");

			player.AddItem(item1.Object);
			player.AddItem(item2.Object);
			Assert.Equal(2, player.Inventory.Count);
		}

		[Fact]
		public void RemoveItem_EmptyList_ShouldReturnFalse()
		{
			var resp = player.RemoveItem("Item 01");
			Assert.False(resp);
		}

		[Fact]
		public void RemoveItem_ItensOnList_ShouldReturnFalse()
		{
			var item2 = new Mock<IInteractableObject>();
			item2.Setup(s => s.Name)
				.Returns("Item 02");

			player.Inventory.Add(item2.Object);

			var resp = player.RemoveItem("Item 01");
			Assert.False(resp);
		}

		[Fact]
		public void RemoveItem_ItensOnList_NotEquipment_ShouldReturnTrue()
		{
			var item2 = new Mock<IInteractableObject>();
			item2.Setup(s => s.Name)
				.Returns("Item 01");

			player.Inventory.Add(item2.Object);

			var resp = player.RemoveItem("Item 01");
			Assert.True(resp);
			Assert.Empty(player.Equipment);
		}

		[Fact]
		public void RemoveItem_ItensOnList_Equipment_ShouldReturnTrue()
		{
			var item2 = new Mock<IInteractableObject>();
			item2.Setup(s => s.Name)
				.Returns("Item 01");

			item2.Setup(s => s.EquipmentType)
				 .Returns(EquipmentType.Weapon);

			player.Inventory.Add(item2.Object);
			player.Equipment.Add(EquipmentType.Weapon, item2.Object);

			var resp = player.RemoveItem("Item 01");
			Assert.True(resp);
			Assert.Empty(player.Equipment);
		}

		[Fact]
		public void EquipItem_EmptyInventory_ShouldReturnFalse()
		{
			var resp = player.EquipItem("Item 01");
			Assert.False(resp);
		}

		[Fact]
		public void EquipItem_Inventory_NotOnInventory_ShouldReeturnFalse()
		{
			var item2 = new Mock<IInteractableObject>();
			item2.Setup(s => s.Name)
				.Returns("Item 01");

			player.Inventory.Add(item2.Object);

			var resp = player.EquipItem("Item 02");
			Assert.False(resp);
		}

		[Fact]
		public void EquipItem_Inventory_OnInventory_NotEquipment_ShouldReturnFalse()
		{
			var item2 = new Mock<IInteractableObject>();
			item2.Setup(s => s.Name)
				.Returns("Item 01");

			player.Inventory.Add(item2.Object);

			var resp = player.EquipItem("Item 01");
			Assert.False(resp);
		}

		[Fact]
		public void EquipItem_OnInventory_ShouldReturnTrue()
		{
			var item2 = new Mock<IInteractableObject>();
			item2.Setup(s => s.Name)
				.Returns("Item 01");

			item2.Setup(s => s.EquipmentType)
				 .Returns(EquipmentType.Weapon);

			player.Inventory.Add(item2.Object);

			var resp = player.EquipItem("Item 01");
			Assert.True(resp);
		}

		[Fact]
		public void EquipItem_OnInventory_Equiped_ShouldChange()
		{
			var item1 = new Mock<IInteractableObject>();
			item1.Setup(s => s.Name)
				.Returns("Item 01");

			item1.Setup(s => s.EquipmentType)
				 .Returns(EquipmentType.Weapon);

			var item2 = new Mock<IInteractableObject>();
			item2.Setup(s => s.Name)
				.Returns("Item 02");

			item2.Setup(s => s.EquipmentType)
				 .Returns(EquipmentType.Weapon);

			player.Inventory.Add(item1.Object);
			player.Inventory.Add(item2.Object);
			player.Equipment.Add(EquipmentType.Weapon, item2.Object);

			var resp = player.EquipItem("Item 01");
			Assert.True(resp);
			Assert.Equal(item1.Object, player.Equipment[EquipmentType.Weapon]);
		}

		[Fact]
		public void TryDoActionOnItem_EmptyInventory_ShouldReturnNull()
		{
			var result = player.TryDoActionOnItem(PlayerCommands.Use, "Item 01");
			Assert.Null(result);
		}

		[Fact]
		public void TryDoActionOnItem_InventoryNotEmpty_NotItem_ShouldReturnNull()
		{
			var item1 = new Mock<IInteractableObject>();
			item1.Setup(s => s.Name)
				.Returns("Item 02");

			player.Inventory.Add(item1.Object);

			var result = player.TryDoActionOnItem(PlayerCommands.Use, "Item 01");
			Assert.Null(result);
		}

		[Fact]
		public void TryDoActionOnItem_InventoryNotEmpty_ShouldReturnNull()
		{
			var item1 = new Mock<IInteractableObject>();
			item1.Setup(s => s.Name)
				.Returns("Item 01");

			item1.Setup(s => s.GetInteraction(It.IsAny<PlayerCommands>()))
				 .Returns((IInteraction)null);

			player.Inventory.Add(item1.Object);

			var result = player.TryDoActionOnItem(PlayerCommands.Use, "Item 01");
			Assert.Null(result);
		}

		[Fact]
		public void TryDoActionOnItem_InventoryNotEmpty_ResponseNull_ShouldReturnNull()
		{
			var interaction = new Mock<IInteraction>();
			var item1 = new Mock<IInteractableObject>();

			item1.Setup(s => s.Name)
				.Returns("Item 01");

			item1.Setup(s => s.GetInteraction(It.IsAny<PlayerCommands>()))
				 .Returns(interaction.Object);

			player.Inventory.Add(item1.Object);

			var result = player.TryDoActionOnItem(PlayerCommands.Use, "Item 01");
			Assert.Null(result);
		}

		[Fact]
		public void TryDoActionOnItem_InventoryNotEmpty_ResponseNull_ShouldReturn()
		{
			var responseAction = new Mock<IResponseAction>();

			var interaction = new Mock<IInteraction>();
			interaction.Setup(s => s.Response)
					  .Returns(responseAction.Object);

			var item1 = new Mock<IInteractableObject>();

			item1.Setup(s => s.Name)
				.Returns("Item 01");

			item1.Setup(s => s.GetInteraction(It.IsAny<PlayerCommands>()))
				 .Returns(interaction.Object);

			player.Inventory.Add(item1.Object);

			var result = player.TryDoActionOnItem(PlayerCommands.Use, "Item 01");
			Assert.NotNull(result);
		}

		[Fact]
		public void AttackEnemy_SameRoll_ReturnNoDamage()
		{
			int roll = 2;
			var enemy = new Mock<IEnemy>();
			enemy.Setup(s => s.GetAttack())
				 .Returns(roll);

			var result = player.AttackEnemy(enemy.Object, roll);
			Assert.Equal(0, result.Item1);
			Assert.Equal(AttackResult.NoDamage, result.Item2);
		}

		[Fact]
		public void AttackEnemy_EnemyWins_DoDamage()
		{
			int enemyRoll = 4;
			int playerRoll = 2;
			var enemy = new Mock<IEnemy>();
			enemy.Setup(s => s.GetAttack())
				 .Returns(enemyRoll);

			var result = player.AttackEnemy(enemy.Object, playerRoll);
			Assert.Equal(2, result.Item1);
			Assert.Equal(AttackResult.EnemyDamagesPlayer, result.Item2);
		}

		[Fact]
		public void AttackEnemy_PlayerWins_NoEquipment_DoDefaultDamage()
		{
			int enemyRoll = 2;
			int playerRoll = 4;
			var enemy = new Mock<IEnemy>();
			enemy.Setup(s => s.GetAttack())
				 .Returns(enemyRoll);

			var result = player.AttackEnemy(enemy.Object, playerRoll);
			Assert.Equal(2, result.Item1);
			Assert.Equal(AttackResult.PlayerDamagesEnemy, result.Item2);
		}

		[Fact]
		public void AttackEnemy_PlayerWins_Equipment_DoDefaultDamage()
		{
			int enemyRoll = 2;
			int playerRoll = 4;
            int weaponDamage = 3;
			var enemy = new Mock<IEnemy>();
			enemy.Setup(s => s.GetAttack())
				 .Returns(enemyRoll);

			var weapon = new Mock<IInteractableObject>();
			weapon.Setup(s => s.Damages)
				  .Returns(new Dictionary<EnemyType, int> { { EnemyType.Default, weaponDamage } });

            player.Equipment.Add(EquipmentType.Weapon, weapon.Object);

			var result = player.AttackEnemy(enemy.Object, playerRoll);
			Assert.Equal(weaponDamage, result.Item1);
			Assert.Equal(AttackResult.PlayerDamagesEnemy, result.Item2);
		}

        [Fact]
		public void AttackEnemy_PlayerWins_Equipment_DoEquipmenttDamage()
		{
			int enemyRoll = 2;
			int playerRoll = 4;
            int weaponDefaultDamage = 3;
            int weaponUndeadDamage = 3;

			var enemy = new Mock<IEnemy>();
			enemy.Setup(s => s.GetAttack())
				 .Returns(enemyRoll);

            enemy.Setup(s => s.EnemyType)
                 .Returns(EnemyType.Undead);

			var weapon = new Mock<IInteractableObject>();
			weapon.Setup(s => s.Damages)
				  .Returns(new Dictionary<EnemyType, int> { { EnemyType.Default, weaponDefaultDamage } });
            weapon.Setup(s => s.Damages)
				  .Returns(new Dictionary<EnemyType, int> { { EnemyType.Undead, weaponUndeadDamage } });

            player.Equipment.Add(EquipmentType.Weapon, weapon.Object);

			var result = player.AttackEnemy(enemy.Object, playerRoll);
			Assert.Equal(weaponUndeadDamage, result.Item1);
			Assert.Equal(AttackResult.PlayerDamagesEnemy, result.Item2);
		}

        [Fact]
        public void ReceiveDamage_LessThanStamina_ReturnFalseForDead()
        {
            int damage = 2;
            var result = player.ReceiveDamage(damage);
            Assert.False(result);
        }

        [Fact]
        public void ReceiveDamage_MoreThanStamina_ReturnTrueForDead()
        {
            int damage = 4;
            var result = player.ReceiveDamage(damage);
            Assert.True(result);
        }
	
        [Fact]
        public void DecreaseStat_ShouldDo()
        {
            player.DecreaseStat(2, Stats.Luck);
			player.DecreaseStat(1, Stats.Gold);
            Assert.Equal(1, player.Luck);
			Assert.Equal(9, player.Gold);
        }

		[Fact]
		public void IncreaseStat_ShouldDo()
		{
			player.IncreaseStat(10, Stats.Gold);
			player.IncreaseStat(10, Stats.Change);
			Assert.Equal(20, player.Gold);
			Assert.Equal(10, player.Change);
		}

		[Fact]
		public void IncreaseStat_NotShouldBeGreaterThanOriginal_ShouldDo()
		{
			player.IncreaseStat(2, Stats.Stamina);
			player.IncreaseStat(10, Stats.Gold);
			Assert.Equal(20, player.Gold);
			Assert.Equal(3, player.Stamina);
		}
    }
}
