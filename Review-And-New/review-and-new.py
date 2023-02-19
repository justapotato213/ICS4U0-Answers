# Simple RPG that teaches the basics of ethics in computing
# e.g. don't look through someones computer, don't cyberbully, 
# don't hack into things etc. 
# stuff to store your decisions throughout the game, and their affects
# e.g. reputation, etc. 


def decision(prompt: str, options: tuple, consequences: tuple):
    '''
    This function automatically handles decisions, as well as its consequences.

    It automatically outputs the prompt, the options they have, as well as handles the input of the user and the exact consequences each choice has.
    
    Parameters
    ----------
    prompt: str
        The exact prompt that triggers the decisions.
    options: tuple
        Tuple containing the possible options that the user has.
    consequences: tuple
        Tuple containing the console output of the decision, the variable the decision affects, and how much it affects it by.
    ----------

    Returns
    ----------
    None
    '''
    # formatting
    print("----------------------------------------")
    # print the prompt
    print(prompt)
    # print the options properly formatted
    for i in range(len(options)):
        print(f'{i+1}) {options[i]}')
    # ask the user for the option, with error checking
    option = None
    while option == None:
        # try and execpt as int could return an error 
        try:
            # asking for proper option
            option = int(input("Enter the number of the option you want to choose. >"))
            # checking if its a valid option
            if option > len(options):
                print("That is not a valid option, please try again!")
                option = None
        except Exception as e:
            # catching any errors that could occur
            if e == TypeError:
                print("That was a proper number, please try again!")
            else:
                print("Something went wrong, try again!")
            option = None
    # printing the conseqeuence
    print(consequences[option][0])
    # change staticstics
    if consequences[option][1] == "rep":
        reputation += consequences[option][2]
    elif consequences[option][1] == "mon":
        money += consequences[option][2]
    # formatting
    print("----------------------------------------")

# statistics setup
reputation = 100
money = 50

# simple starting screen
print("Hello, and welcome to this small RPG (Role Playing Game) about computing!")
print("In this game, you'll be a worker at the office, and will be put into situations with many different dilemmas.")
print("Lets start with the basics:")

# Input for basic story stuff
name = input("What is your name? >")
bossName = input("What is your bosses name? >")
coworkerName = input("What is your coworkers name? >")

# Actual story
print(f'Hello {name}, welcome to your new job at the office!')
print(f'I, {bossName}, will be introducing you to your new job here!')
print(f'<You are led into a room full of cubicles>')
print(f"Over here, you can see the main office, where you'll be working all day. ")
print(f"<You notice many people typing on their computers>")
print(f'Over in that corner is the breakroom, which comes with our very own water cooler!')
print(f'<In the corner, you see a single water cooler, and a tiny chair beside it, as well as a sign labelled breakroom.>')
print(f"Now over here is your desk, where you'll be working all day.")
print(f'Now get to work!')

# decisions
decision()