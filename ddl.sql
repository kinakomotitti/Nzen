-- Project Name : noname
-- Date/Time    : 2019/05/02 14:27:42
-- RDBMS Type   : kinakomotittiQL
-- Application  : A5:SQL Mk-2

/*
  BackupToTempTable, RestoreFromTempTable疑似命令が付加されています。
  これにより、drop table, create table 後もデータが残ります。
  この機能は一時的に $$TableName のような一時テーブルを作成します。
*/

-- イベント情報詳細
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
    IS 'イベント情報詳細';

COMMENT ON COLUMN public.tran_event_info_detail.serial_no
    IS 'ID';

COMMENT ON COLUMN public.tran_event_info_detail.group_id
    IS 'グループコード';

COMMENT ON COLUMN public.tran_event_info_detail.group_entry_date
    IS 'グループ登録日時';

COMMENT ON COLUMN public.tran_event_info_detail.info_type
    IS '情報種別';

COMMENT ON COLUMN public.tran_event_info_detail.data
    IS '内容';

COMMENT ON COLUMN public.tran_event_info_detail.insert_date
    IS '登録日時';

COMMENT ON COLUMN public.tran_event_info_detail.insert_user
    IS '登録者';

COMMENT ON COLUMN public.tran_event_info_detail.update_date
    IS '更新日時';

COMMENT ON COLUMN public.tran_event_info_detail.update_user
    IS '更新者';

-- イベント情報概要
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

-- コードマスタ
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

comment on table tran_event_info_detail is 'イベント情報詳細';
comment on column tran_event_info_detail.group_id is 'グループコード';
comment on column tran_event_info_detail.group_entry_date is 'グループ登録日時';
comment on column tran_event_info_detail.info_type is '情報種別';
comment on column tran_event_info_detail.data is '内容';
comment on column tran_event_info_detail.insert_date is '登録日時';
comment on column tran_event_info_detail.insert_user is '登録者';
comment on column tran_event_info_detail.update_date is '更新日時';
comment on column tran_event_info_detail.update_user is '更新者';

comment on table tran_event_overview is 'イベント情報概要';
comment on column tran_event_overview.event_id is 'グループコード';
comment on column tran_event_overview.event_entry_date is 'グループ登録日時';
comment on column tran_event_overview.event_host_user_name is '主催者';
comment on column tran_event_overview.insert_date is '登録日時';
comment on column tran_event_overview.insert_user is '登録者';
comment on column tran_event_overview.update_date is '更新日時';
comment on column tran_event_overview.update_user is '更新者';

comment on table mst_code is 'コードマスタ';
comment on column mst_code.code_id is 'code_id';
comment on column mst_code.code_name is 'code_name';
comment on column mst_code.insert_date is '登録日時';
comment on column mst_code.insert_user is '登録者';
comment on column mst_code.update_date is '更新日時';
comment on column mst_code.update_user is '更新者';
