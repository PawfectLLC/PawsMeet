# %%
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
from metaphone import doublemetaphone
import numpy as np

from joblib import Parallel, delayed
import functools
from cachetools import cached, TTLCache
import sys

# %%
# Example database of pets with 'pid', 'breeds', and 'about'
pets_database = [
    {'pid': 900000000000000000000001, 'breeds': 'Labrador', 'about': 'Friendly and playful puppy.'},
    {'pid': 2, 'breeds': 'Siamese', 'about': 'Independent and curious cat.'},
    {'pid': 3, 'breeds': 'Beagle', 'about': 'Gentle and determined dog, great with kids.'},
    {'pid': 4, 'breeds': 'Persian', 'about': 'Quiet and plush cat with a sweet expression.'},
    {'pid': 5, 'breeds': 'Bulldog', 'about': 'Docile and willful dog, known for its loose, wrinkled skin.'},
    {'pid': 6, 'breeds': 'Maine Coon', 'about': 'Friendly giant with tufted ears, enjoying both play and rest.'},
    {'pid': 7, 'breeds': 'Poodle', 'about': 'Intelligent and active dog, known for its curly, hypoallergenic coat.'},
    {'pid': 8, 'breeds': 'Sphinx', 'about': 'A hairless and energetic cat, affectionate with family.'},
    {'pid': 9, 'breeds': 'Golden Retriever', 'about': 'Intelligent and friendly, perfect family dog.'},
    {'pid': 10, 'breeds': 'Ragdoll', 'about': 'Blue-eyed cat breed, known for its docile nature and affectionate behavior.'}
]

# %%
"""
phonetic_code and phonetic_similarity:

These functions are used to process and compare text based on how it sounds rather than how it is spelled. 

1. Improving Search Functionality: Phonetic coding enables the system to match user queries with phonetically similar terms, accommodating users who may be unsure of spellings but can articulate the word.

2. Data Cleaning: Phonetic coding is useful for consolidating data entries that have different spellings but the same pronunciation, which is particularly common with names and geographical locations.

3. Voice Recognition Systems: By focusing on pronunciation rather than spelling, phonetic codes improve the accuracy of voice recognition technologies, allowing for more flexible user interactions.

4. Natural Language Processing (NLP): Phonetic coding assists in recognizing spoken keywords in speech recognition, effectively accommodating for variations in pronunciation due to accents or speech disorders.
"""

# Function to get the phonetic code of text
def phonetic_code(text):
    words = text.split()
    codes = [doublemetaphone(word) for word in words]
    # Flatten the list of tuples and remove None values
    return [code for sublist in codes for code in sublist if code]

# Function to calculate phonetic similarity
def phonetic_similarity(input_codes, pet_codes):
    # Count the number of phonetic codes that match
    return sum(1 for code in input_codes if code in pet_codes) / len(pet_codes) if pet_codes else 0

# Function to calculate matching scores and return scores with pet IDs
def calculate_matching_scores(input_text, pets, max_length=50):
    if len(input_text) > max_length:
        raise ValueError(f"The input text is too long. Maximum length is {max_length} characters.")
    
    print("Input text length is within limits")
    # Get phonetic codes for the input text
    input_codes = phonetic_code(input_text)

    print("Phonetic codes generated for input text")
    
    # Combine 'about' and 'breeds' from the database into a list and calculate phonetic codes
    combined_texts = [pet['about'] + " " + pet['breeds'] for pet in pets]
    pet_codes_list = [phonetic_code(text) for text in combined_texts]

    # Add the input text to the list for vectorization
    combined_texts.append(input_text)

    # Create the TfidfVectorizer object and convert the combined text into TF-IDF vectors
    vectorizer = TfidfVectorizer()
    tfidf_matrix = vectorizer.fit_transform(combined_texts)

    # Calculate cosine similarities for TF-IDF vectors
    cosine_similarities = cosine_similarity(tfidf_matrix[-1], tfidf_matrix[:-1]).flatten().tolist()

    # Calculate phonetic similarities
    phonetic_similarities = [phonetic_similarity(input_codes, pet_codes) for pet_codes in pet_codes_list]

    # Combine TF-IDF and phonetic similarities
    # Here you may decide on a strategy to combine these scores, e.g., averaging them
    combined_scores = [(pet['pid'], (tfidf_score + phonetic_score) / 2)
                       for pet, tfidf_score, phonetic_score in zip(pets, cosine_similarities, phonetic_similarities)]

    """
    Further improvement: Optimized Combination of Scores 
    The way we combine TF-IDF and phonetic scores could be optimized based on the importance of each feature. 
    This could be a weighted average instead of a simple average if one of the scores is deemed more important.
    """
    
    return combined_scores

def top_five_petID(scored_pets):
    scored_pets.sort(key = lambda x: x[1], reverse=True)
    top_five_pets = scored_pets[:1]
    pet_ids = [pid for pid, _ in top_five_pets]
    return pet_ids

if __name__ == "__main__":
    # Input text from the user
    #user_input = input("Please enter the description and breed of the pet you're looking for: ")
    
    user_input = " ".join(sys.argv[1:])

    # Execute the matching score calculation and handle potential errors
    try:
        # Calculate matching scores
        scored_pets = calculate_matching_scores(user_input, pets_database)

        # Sort the scores and choose the top five records 
        top_fiveIds = top_five_petID(scored_pets)

        # Print the matching score and pet ID for each pet
        for pid, score in scored_pets:
            print(f"Pet ID {pid}: Score = {score:.2f}")
        
        print("Top five pet IDs:"," ".join(map(str, top_fiveIds)))
    except ValueError as e:
        print(e)


