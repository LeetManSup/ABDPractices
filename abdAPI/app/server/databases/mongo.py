import os
import motor.motor_asyncio
from bson.objectid import ObjectId

MONGO_DB_URL = os.getenv('MONGO_DB_URL')

client = motor.motor_asyncio.AsyncIOMotorClient(MONGO_DB_URL)

database = client.speakers

speaker_collection = database.get_collection("speakers")


# helpers

def speaker_helper(speaker) -> dict:
    return {
        "id": str(speaker["_id"]),
        "speaker_aux": speaker["speaker_aux"],
        "speaker_bluetooth": speaker["speaker_bluetooth"],
        "speaker_count": speaker["speaker_count"],
        "speaker_manufacturer": speaker["speaker_manufacturer"],
        "speaker_name": speaker["speaker_name"],
        "speaker_price": speaker["speaker_price"]
    }


async def retrieve_speakers():
    speakers = []
    async for speaker in speaker_collection.find():
        speakers.append(speaker_helper(speaker))
    return speakers


async def add_speaker(speaker_data: dict) -> dict:
    speaker = await speaker_collection.insert_one(speaker_data)
    new_speaker = await speaker_collection.find_one({"_id": speaker.inserted_id})
    return speaker_helper(new_speaker)


async def update_speaker(id: str, data: dict):
    if len(data) < 1:
        return False
    speaker = await speaker_collection.find_one({"_id": ObjectId(id)})
    if speaker:
        updated_speaker = await speaker_collection.update_one(
            {"_id": ObjectId(id)}, {"$set": data}
        )
        if updated_speaker:
            return True
        return False


async def delete_speaker(id: str):
    speaker = await speaker_collection.find_one({"_id": ObjectId(id)})
    if speaker:
        await speaker_collection.delete_one({"_id": ObjectId(id)})
        return True
