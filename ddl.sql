-- Project Name : noname
-- Date/Time    : 2019/04/30 18:28:04
-- Author       : sugane
-- RDBMS Type   : PostgreSQL
-- Application  : A5:SQL Mk-2

/*
  BackupToTempTable, RestoreFromTempTable�^�����߂��t������Ă��܂��B
  ����ɂ��Adrop table, create table ����f�[�^���c��܂��B
  ���̋@�\�͈ꎞ�I�� $$TableName �̂悤�Ȉꎞ�e�[�u�����쐬���܂��B
*/

-- �C�x���g���ڍ�
--* RestoreFromTempTable
create table tran_event_info_detail (
  event_code character varying not null
  , info_type character varying
  , data character varying
  , insert_date timestamp
  , insert_user character varying
  , update_date timestamp
  , update_user character varying
  , constraint tran_event_info_detail_PKC primary key (event_code)
) ;

-- �C�x���g���T�v
--* RestoreFromTempTable
create table tran_event_overview (
  event_id serial not null
  , event_entry_date timestamp
  , event_name character varying
  , event_host_user_name character varying
  , insert_date timestamp
  , insert_user character varying
  , update_date timestamp
  , update_user character varying
  , constraint tran_event_overview_PKC primary key (event_id)
) ;

-- �R�[�h�}�X�^
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
comment on column tran_event_info_detail.event_code is '�C�x���g�R�[�h';
comment on column tran_event_info_detail.info_type is '�����';
comment on column tran_event_info_detail.data is '���e';
comment on column tran_event_info_detail.insert_date is '�o�^����';
comment on column tran_event_info_detail.insert_user is '�o�^��';
comment on column tran_event_info_detail.update_date is '�X�V����';
comment on column tran_event_info_detail.update_user is '�X�V��';

comment on table tran_event_overview is '�C�x���g���T�v';
comment on column tran_event_overview.event_id is '�C�x���g�R�[�h';
comment on column tran_event_overview.event_entry_date is '�C�x���g�o�^����';
comment on column tran_event_overview.event_name is '�C�x���g��';
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
