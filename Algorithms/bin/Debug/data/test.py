import json

f = open('names.json')

data = json.load(f)

print(len(data))

f.close()