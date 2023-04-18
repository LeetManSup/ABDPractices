from typing import Optional
from pydantic import BaseModel, Field


class SpeakerSchema(BaseModel):
    speaker_aux: bool = Field(..., description="Whether this speaker has an aux")
    speaker_bluetooth: bool = Field(..., description="Whether this speaker has an bluetooth")
    speaker_count: int = Field(..., description="How many speakers are left in stock")
    speaker_manufacturer: str = Field(..., description="The manufacturer of speaker", max_length=100)
    speaker_name: str = Field(..., description="The name (model) of speaker", max_length=100)
    speaker_price: int = Field(..., description="How much does this speaker cost")


    class Config:
        schema_extra = {
            "example": {
                "speaker_aux": True,
                "speaker_bluetooth": False,
                "speaker_count": 10,
                "speaker_manufacturer": "JBM",
                "speaker_name": "Uncharge 100",
                "speaker_price": 11990
            }
        }


class UpdateSpeakerModel(BaseModel):
    speaker_aux: Optional[bool]
    speaker_bluetooth: Optional[bool]
    speaker_count: Optional[int]
    speaker_manufacturer: Optional[str]
    speaker_name: Optional[str]
    speaker_price: Optional[int]

    class Config:
        schema_extra = {
            "example": {
                "speaker_aux": True,
                "speaker_bluetooth": True,
                "speaker_count": 5,
                "speaker_manufacturer": "JBM",
                "speaker_name": "Uncharge 200",
                "speaker_price": 14990
            }
        }


def response_model(data, message):
    return {
        "data": [data],
        "code": 200,
        "message": message,
    }


def error_response_model(error, code, message):
    return {"error": error, "code": code, "message": message}
