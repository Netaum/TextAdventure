import re
import json
# with open("gameText.txt", "r") as file:
# 	newText = ""
# 	index = 0
# 	for line in file:
# 		if len(line) <= 2:
# 			newText += line
# 		else:
# 			newText += line.rstrip()

# 	newText = ".\n".join(newText.split("."))
	

# with open("newText.txt", "w+") as w:
# 	w.write(newText)

# print("end")

# pattern = re.compile("\[([^:]+):([^{]+){([^}]+)}\]")
# txt = ""
# with open("formated.txt", "r") as file:
# 	txt = file.read()

# for (number, text) in re.findall(pattern, txt):
# 	fileName = f"{number}.txt"
# 	with open(fileName, "w+") as f:
# 		f.write(text.strip())
# "type": "attributeCheck",
# 						"attribute": "stamina",
# 						"checkCondition": "lessOrEqual",
# 						"value": "6",
# 						"nextScene": 271

class MoveCondition:
	def __init__(self,
				 type,
				 attribute,
				 checkCondition,
				 value,
				 nextScene):
		self.type = type
		self.attribute = attribute
		self.checkCondition = checkCondition
		self.value = value
		self.nextScene = nextScene


class Enemy:
	def __init__(self,
				 name,
				 skill,
				 stamina):
		self.name = name
		self.skill = skill
		self.stamina = stamina
		self.moveConditions = None

	def add_move_condition(self, moveCondition):
		if not self.moveConditions:
			self.moveConditions = []
		self.moveConditions.append(moveCondition)

class Spawn:
	def __init__(self,
				 name,
				 numberDice,
				 plus,
				 skill,
				 stamina,
				 nextScene,
				 description):
		self.name = name
		self.numberOfDice = numberDice
		self.plus = plus
		self.skill = skill
		self.stamina = stamina
		self.nextScene = nextScene
		self.description = description

class Exit:
	def __init__(self,
				 description,
				 key,
				 sceneId):
		self.description = description
		self.key = key
		self.sceneId = sceneId

class Condition:
	def __init__(self,
				 type,
				 checkCondition,
				 attribute,
				 value,
				 sourceDescription):
		self.type = type
		self.checkCondition = checkCondition
		self.attribute = attribute
		self.value = value
		self.sourceDescription = sourceDescription

class Scene:
	def __init__(self,
				 id):
		self.id = id
		self.fileDescription = True
		self.nextScene = None
		self.conditions = None
		self.exits = None
		self.spawns = None
		self.enemies = None

	def add_exit(self, exit):
		if not self.exits:
			self.exits = []
		self.exits.append(exit)

	def add_condition(self, condition):
		if not self.conditions:
			self.conditions = []
		self.conditions.append(condition)

	def add_next_scene(self, nextScene):
		self.nextScene = nextScene
	
	def add_spawn(self, spawn):
		if not self.spawns:
			self.spawns = []
		self.spawns.append(spawn)

	def add_enemy(self, enemy):
		if not self.enemies:
			self.enemies = []
		self.enemies.append(enemy)


chapterPattern = re.compile("\[([^:]+):([^{]+){([^}]+)}\]")
conditionPattern = re.compile("-c:\(([^:]*):([^:]*):([^:]*):([^:]*):([^:]*)\)")
scenePattern = re.compile("-n:(.+)")
exitPattern = re.compile("-q:([^\(]+)\((\w+):(\w+)\)")
spawnPattern = re.compile("-s:\(([^:]*):([^:]*):([^:]*):([^:]*):([^:]*):([^:]*):([^:]*)\)")
enemyPattern = re.compile("-e:\(([^:]*):([^:]*):([^:]*)\)")
moveCPattern = re.compile("-ec:\(([^:]*):([^:]*):([^:]*):([^:]*):([^:]*)\)")

txt = ""
scenes = {}
with open("formated.txt", "r") as file:
	txt = file.read()
	
for (number, text, exits) in re.findall(chapterPattern, txt):
	fileName = f"{number}.txt"
	#with open(fileName, "w+") as f:
	#	f.write(text.strip())
	s = Scene(number)
	for line in exits.splitlines():

		if line.startswith("-c"):
			(cType, cCheck, cStat, cValue, cDesc) = re.findall(conditionPattern, line)[0]
			c = Condition(cType,cCheck,cStat,cValue,cDesc)
			s.add_condition(c)

		if line.startswith("-n"):
			(sNext) = re.findall(scenePattern, line)[0]
			s.add_next_scene(sNext)

		if line.startswith("-q"):
			(qDesc,qKey,qScene) = re.findall(exitPattern, line)[0]
			q = Exit(qDesc,qKey,qScene)
			s.add_exit(q)

		if line.startswith("-s"):
			(sName,sDice,sPlus,sSkill,sStamina,sScene,sDesc) = re.findall(spawnPattern, line)[0]
			spw = Spawn(sName, sDice, sPlus, sSkill, sStamina, sScene, sDesc)
			s.add_spawn(spw)
		
		if line.startswith("-e:"):
			(name, skill, stamina) = re.findall(enemyPattern, line)[0]
			e = Enemy(name, skill, stamina)
			s.add_enemy(e)

		if line.startswith("-ec:"):
			(mType,attribute,checkCondition,value,nextScene) = re.findall(moveCPattern, line)[0]
			mc = MoveCondition(mType,attribute,checkCondition,value,nextScene)
			e.add_move_condition(mc)

	scenes[number] = s

print('oi')
print(json.dumps(scenes, default=lambda o: o.__dict__, sort_keys=True, indent=4))

