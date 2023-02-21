#-----------------------------------------------------------------------------
# Name:        Ethical Programming RPG
# Purpose:     Small RPG that teaches the user about ethical computing.
#
# Author:      Tony Lin
# Created:     19-Feb-2023
# Updated:     19-Feb-2023
#-----------------------------------------------------------------------------

# Simple RPG that teaches the basics of ethics in computing
# e.g. don't look through someones computer, don't cyberbully, 
# don't hack into things etc. 
# stuff to store your decisions throughout the game, and their affects
# e.g. reputation, etc. 

import random, time

def decision(prompt: str, options: list, consequences: list):
    '''
    This function automatically handles decisions, as well as its consequences.
    It automatically outputs the prompt, the options they have, as well as handles the input of the user and the exact consequences each choice has.
    
    Parameters
    ----------
    prompt: str
        The exact prompt that triggers the decisions.
    options: list
        List containing the possible options that the user has.
    consequences: list
        List of lists containing the console output of the decision, the variable the decision affects, and how much it affects it by.
    ----------
    Returns
    ----------
    None
    '''
    # setup
    global reputation
    global money

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
            option = int(input("Enter the number of the option you want to choose. =>"))
            # checking if its a valid option
            if option > len(options):
                print("That is not a valid option, please try again!")
                option = None
        except Exception as e:
            # catching any errors that could occur
            if e == TypeError:
                print("That was not a proper number, please try again!")
            else:
                print("Something went wrong, try again!")
            option = None
    # printing the conseqeuence
    print(consequences[option-1][0])
    # change staticstics
    if consequences[option-1][1] == "rep":
        reputation += consequences[option-1][2]
    elif consequences[option-1][1] == "mon":
        money += consequences[option-1][2]
    # formatting
    print("----------------------------------------")

# statistics setup
reputation = 100
money = 50

# time setup
startTime = time.time()

# simple starting screen
print("Hello, and welcome to this small RPG (Role Playing Game) about computing!")
print("In this game, you'll be a worker at the office, and will be put into situations with many different dilemmas.")
print("Lets start with the basics:")

# Input for basic story stuff
name = input("What is your name? =>")
bossName = input("What is your bosses name? =>")

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
decision("As you work throughout the day, you notice that your coworker has left their computer on. Do you: ", ["Snoop around their computer.", "Leave it alone.", "Tell them they left it on."], [["As you look around their computer, you find some boring files and a spreadsheet open. What you did not notice is that your coworker has come back from their break, and has noticed you. (Respecting privacy is one of the core ethical foundations of computing, and by snooping on someones computer you have violated it.)", "rep", -10], ["You leave their computer alone, and focus on your work. (You respected their privacy, which is one of the core foundations of ethical computing.)", "rep", 0], ["You find your coworker, and tell them that their computer is on. They thank you, and go back to turn it off. (In this case, you have respected their privacy, which is one of the core ways you can use computers ethically.)", "rep", 10]])

decision("Later on in the day, you discover a security flaw in a piece of software you are using Do you: ", ["Actively exploit the flaw and steal information.", "Report the flaw to the makers of the software."], [["You use the flaw in the software, and use it to harvest information but unfortunately, someone notices and fixes it immediately.", "rep", -10], ["You report the flaw to the software makers, and they give you a good reward, as well as thanking you for your efforts", "rep", 10]])
print("In this case, reporting dangerous flaws to the makers of a software is a strategy to ethically use computers, as you potentially stop someone else from potentially doing something malicious on purpose.")

decision("When you get home, you discover that a TV show that your friend recommended isn't on netflix. Do you:", ["Pirate it from an online website.", "Buy it for $20.", "Not watch it."], [["You pirate the show, and your friend soon realize what you have done.", "rep", -10], ["You buy the show, and have a fun time watching it", "mon", -10], ["You decide to watch something on Youtube instead.", "mon", 0]])
print("Another ethical computing practice is to not steal things. This includes things such as money, but also includes things such as shows or anything that is usually a paid product.")

decision("Even later in the day, you see someone bully a kid online. Do you: ", ["Join in on the bullying.", "Try to intervene and stop them.", "Ignore the kid"], [["You decide to join in, and leave a mean comment on the page. Unbeknownst to you, one of your coworkers noticed that you lef this comment.", "rep", -10], ["You decide to help the kid, and stop the bullying.", "rep", 10], ["You ignore the kid, and go on with your day", "rep", 0]])
print("In computing, purposefully harming someone is also ethically wrong. This includes situations such as cyberbullying, or trying to hurt someone even if it isn't physically.")

print("----------------------------------------")

if reputation >= 100:
    print("Congratulations, you made lots of decisions that were ethically good, and you are well prepared to practice these strategies in the real world!")
elif reputation < 80 and reputation > 100: 
    print("You made some ethically dubious choices, and so you can still learn a lot about ethically using computers")
else:
    print("You made all the wrong choices, and should really avoid doing this in real life, as otherwise you are abusing computers, and could possibly get into trouble.")

print("----------------------------------------")

# quiz
# 4 questions based on the thing, choose one randomly 
print("Thats the game finished, now for a quiz!")
questions = ["Should you snoop through someones computer?", "Is stealing an idea from someone online still ethically bad?", "Why should you report flaws to people who make software?", "If you use a computer and only hurt them online, is it still a non-ethical use of a computer?"]
number = random.randint(0, 100)
# int division to get more random number
number = number // 13
# modulo to get a correct index
number = number % 4
# actual quiz
if number == 0:
    decision(questions[number], ["Yes", "No"], [["Correct! You shouldn't snoop through someones computer as it could include private files that they don't want you to see, and so you are violating their privacy which is ethically bad.", "rep", 0], ["Wrong answer! You shouldn't snoop through someones computer as it could include private files that they don't want you to see, and so you are violating their privacy which is ethically bad.", "rep", 0]])
elif number == 1:   
    decision(questions[number], ["Yes", "No"], [["Correct! Even if you aren't stealing something physical, computers are not to be used for stealing, and so doing this is still malicous and therefore ethically bad.", "rep", 0], ["Wrong Answer! Even if you aren't stealing something physical, computers are not to be used for stealing, and so doing this is still malicous and therefore ethically bad.", "rep", 0]])
elif number == 2:
    decision(questions[number], ["Trick question! You shouldn't report them so you can abuse the flaw!", "You should report them so that you can make money.", "You should report them so that they can stop any malicous use of this flaw."], [["Wrong Answer! You should report them so that they can stop any malicous use of this flaw.", "rep", 0], ["Wrong Answer! You should report them so that they can stop any malicous use of this flaw.", "rep", 0], ["Correct! You should report them so that they can stop any malicous use of this flaw.", "rep", 0]])
elif number == 3:
    decision(questions[number], ["Yes", "No"], [["Correct! Computers should never be used to harm anyone, even if you are not physically harming them in person.", "rep", 0], ["Wrong Answer! Computers should never be used to harm anyone, even if you are not physically harming them in person.", "rep", 0]])

endTime = time.time()

# convert into hours, minutes, seconds
totalTime = endTime - startTime

hours = totalTime // 3600
totalTime -= (hours * 3600)
minutes = totalTime // 60
totalTime -= (minutes * 60)
seconds = int(totalTime % 60)

print(f"It took you {hours}h {minutes}m and {seconds}s to complete this game!")

