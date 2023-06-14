import openai

openai.api_key  = ''

def get_completion_from_messages(messages, 
                                 model="gpt-3.5-turbo-0613", 
                                 temperature=0, 
                                 max_tokens=3000):
    response = openai.ChatCompletion.create(
        model=model,
        messages=messages,
        temperature=temperature, 
        max_tokens=max_tokens, 
    )
    return response.choices[0].message["content"]

delimiter = "####"

system_message = f"""
    You are a random word bot with a sense of humour. Respond with a single funny word based on the users input.
"""

user_message = f"""
    AI, AI, on the net, tell me who's the nicest pet.
"""

messages =  [  
{'role':'system', 
 'content': system_message},    
{'role':'user', 
 'content': f"{delimiter}{user_message}{delimiter}"},  
] 

response = get_completion_from_messages(messages)

print(response)