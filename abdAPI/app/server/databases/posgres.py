import os
from sqlalchemy import Column, Integer, MetaData, String, Table, create_engine

from databases import Database

POSTGRES_DB_URL = os.getenv('POSTGRES_DB_URL')

engine = create_engine(POSTGRES_DB_URL)
metadata = MetaData()

employee = Table(
    'employee',
    metadata,
    Column('id', Integer, autoincrement=True, primary_key=True, unique=True, nullable=False),
    Column('name', String(30), nullable=False),
    Column('surname', String(30), nullable=False),
    Column('phone', String(12), unique=True, nullable=False),
    Column('email', String(100), unique=True, nullable=False)
)

postgres_db = Database(POSTGRES_DB_URL)
