-- Project Name : noname
-- Date/Time    : 2019/05/02 14:27:42
-- RDBMS Type   : kinakomotittiQL
-- Application  : A5:SQL Mk-2

/*
  BackupToTempTable, RestoreFromTempTable�^�����߂��t������Ă��܂��B
  ����ɂ��Adrop table, create table ����f�[�^���c��܂��B
  ���̋@�\�͈ꎞ�I�� $$TableName �̂悤�Ȉꎞ�e�[�u�����쐬���܂��B
*/

-- �C�x���g���ڍ�
--* BackupToTempTable
drop table if exists tran_event_info_detail cascade;

-- Table: public.tran_event_info_detail

DROP TABLE if exists public.tran_event_info_detail;

CREATE TABLE public.tran_event_info_detail
(
    serial_no serial NOT NULL,
    group_id character varying COLLATE pg_catalog."default" NOT NULL,
    group_entry_date date NOT NULL,
    info_type character varying COLLATE pg_catalog."default",
    data character varying COLLATE pg_catalog."default",
    insert_date timestamp without time zone,
    insert_user character varying COLLATE pg_catalog."default",
    update_date timestamp without time zone,
    update_user character varying COLLATE pg_catalog."default",
    CONSTRAINT tran_event_info_detail_pkc PRIMARY KEY (serial_no)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.tran_event_info_detail
    OWNER to kinakomotitti;
COMMENT ON TABLE public.tran_event_info_detail
    IS '�C�x���g���ڍ�';

COMMENT ON COLUMN public.tran_event_info_detail.serial_no
    IS 'ID';

COMMENT ON COLUMN public.tran_event_info_detail.group_id
    IS '�O���[�v�R�[�h';

COMMENT ON COLUMN public.tran_event_info_detail.group_entry_date
    IS '�O���[�v�o�^����';

COMMENT ON COLUMN public.tran_event_info_detail.info_type
    IS '�����';

COMMENT ON COLUMN public.tran_event_info_detail.data
    IS '���e';

COMMENT ON COLUMN public.tran_event_info_detail.insert_date
    IS '�o�^����';

COMMENT ON COLUMN public.tran_event_info_detail.insert_user
    IS '�o�^��';

COMMENT ON COLUMN public.tran_event_info_detail.update_date
    IS '�X�V����';

COMMENT ON COLUMN public.tran_event_info_detail.update_user
    IS '�X�V��';

-- �C�x���g���T�v
--* BackupToTempTable
drop table if exists tran_event_overview cascade;

--* RestoreFromTempTable
create table tran_event_overview (
  event_id character varying not null
  , event_entry_date date not null
  , event_host_user_name character varying not null
  , insert_date timestamp
  , insert_user character varying
  , update_date timestamp
  , update_user character varying
  , constraint tran_event_overview_PKC primary key (event_id,event_entry_date)
) ;

-- �R�[�h�}�X�^
--* BackupToTempTable
drop table if exists mst_code cascade;

--* RestoreFromTempTable
create table mst_code (
  code_id character varying not null
  , code_name character varying
  , insert_date timestamp
  , insert_user character varying
  , update_date timestamp
  , update_user character varying
  , constraint mst_code_PKC primary key (code_id)
) ;

comment on table tran_event_info_detail is '�C�x���g���ڍ�';
comment on column tran_event_info_detail.group_id is '�O���[�v�R�[�h';
comment on column tran_event_info_detail.group_entry_date is '�O���[�v�o�^����';
comment on column tran_event_info_detail.info_type is '�����';
comment on column tran_event_info_detail.data is '���e';
comment on column tran_event_info_detail.insert_date is '�o�^����';
comment on column tran_event_info_detail.insert_user is '�o�^��';
comment on column tran_event_info_detail.update_date is '�X�V����';
comment on column tran_event_info_detail.update_user is '�X�V��';

comment on table tran_event_overview is '�C�x���g���T�v';
comment on column tran_event_overview.event_id is '�O���[�v�R�[�h';
comment on column tran_event_overview.event_entry_date is '�O���[�v�o�^����';
comment on column tran_event_overview.event_host_user_name is '��Î�';
comment on column tran_event_overview.insert_date is '�o�^����';
comment on column tran_event_overview.insert_user is '�o�^��';
comment on column tran_event_overview.update_date is '�X�V����';
comment on column tran_event_overview.update_user is '�X�V��';

comment on table mst_code is '�R�[�h�}�X�^';
comment on column mst_code.code_id is 'code_id';
comment on column mst_code.code_name is 'code_name';
comment on column mst_code.insert_date is '�o�^����';
comment on column mst_code.insert_user is '�o�^��';
comment on column mst_code.update_date is '�X�V����';
comment on column mst_code.update_user is '�X�V��';
