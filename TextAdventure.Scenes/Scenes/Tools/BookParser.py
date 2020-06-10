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
				 type,
				 nextScene,
				 numberOfDice,
				 enemyCountModifier,
				 skill,
				 stamina):
		self.name = name
		self.numberOfDice = int(numberOfDice)
		self.enemyCountModifier = int(enemyCountModifier)
		self.skill = int(skill)
		self.stamina = int(stamina)
		self.nextScene = nextScene

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
				 id,
				 description):
		self.id = id
		self.description = description
		self.nextScene = None
		self.conditions = None
		self.exits = None
		self.enemies = None
		self.enemySpawner = None

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
	
	def set_spawn(self, spawn):
		self.enemySpawner = spawn

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
	#fileName = f"{number}.txt"
	#with open(fileName, "w+") as f:
	#	f.write(text.strip())
	description = text.strip()
	s = Scene(number, description)
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
			(sName,sType,sDice,sPlus,sSkill,sStamina,sScene) = re.findall(spawnPattern, line)[0]
			spw = Spawn(sName, sType, sScene, sDice, sPlus, sSkill, sStamina)
			s.set_spawn(spw)
		
		if line.startswith("-e:"):
			(name, skill, stamina) = re.findall(enemyPattern, line)[0]
			e = Enemy(name, skill, stamina)
			s.add_enemy(e)

		if line.startswith("-ec:"):
			(mType,attribute,checkCondition,value,nextScene) = re.findall(moveCPattern, line)[0]
			mc = MoveCondition(mType,attribute,checkCondition,value,nextScene)
			e.add_move_condition(mc)

	scenes[number] = s
	fileName = f"{number}.json"
	with open(fileName, "w+") as f:
		json_text = json.dumps(s, default=lambda o: o.__dict__, sort_keys=True, indent=4)
		f.write(json_text)

print('oi')
#print()

