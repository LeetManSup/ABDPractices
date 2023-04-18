from pydantic import BaseModel, EmailStr
from typing import Optional


class EmployeeIn(BaseModel):
    name: str
    surname: str
    phone: str
    email: EmailStr


class EmployeeOut(EmployeeIn):
    id: int


class EmployeeUpdate(EmployeeIn):
    name: Optional[str] = None
    surname: Optional[str] = None
    phone: Optional[str] = None
    email: Optional[EmailStr] = None

