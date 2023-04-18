from fastapi import APIRouter, Body
from fastapi.encoders import jsonable_encoder

from app.server.databases.mongo import (
    add_speaker,
    delete_speaker,
    retrieve_speakers,
    update_speaker,
)
from app.server.models.speaker_model import (
    error_response_model,
    response_model,
    SpeakerSchema,
    UpdateSpeakerModel,
)

router = APIRouter()


@router.post("/", response_description="Speaker data added into the database")
async def add_speaker_data(speaker: SpeakerSchema = Body(...)):
    speaker = jsonable_encoder(speaker)
    new_speaker = await add_speaker(speaker)
    return response_model(new_speaker, "Speaker added successfully.")


@router.get("/", response_description="Speakers retrieved")
async def get_speakers():
    speakers = await retrieve_speakers()
    if speakers:
        return response_model(speakers, "Speakers data retrieved successfully")
    return response_model(speakers, "Empty list returned")


@router.put("/{id}")
async def update_speaker_data(id: str, req: UpdateSpeakerModel = Body(...)):
    req = {k: v for k, v in req.dict().items() if v is not None}
    updated_speaker = await update_speaker(id, req)
    if updated_speaker:
        return response_model(
            "Speaker with ID: {} name update is successful".format(id),
            "Speaker name updated successfully",
        )
    return error_response_model(
        "An error occurred",
        404,
        "There was an error updating the speaker data.",
    )


@router.delete("/{id}", response_description="Speaker data deleted from the database")
async def delete_speaker_data(id: str):
    deleted_speaker = await delete_speaker(id)
    if deleted_speaker:
        return response_model(
            "Speaker with ID: {} removed".format(id), "Speaker deleted successfully"
        )
    return error_response_model(
        "An error occurred", 404, "Speaker with id {0} doesn't exist".format(id)
    )

