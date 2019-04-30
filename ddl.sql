-- Project Name : noname
-- Date/Time    : 2019/04/30 18:28:04
-- Author       : sugane
-- RDBMS Type   : PostgreSQL
-- Application  : A5:SQL Mk-2

/*
  BackupToTempTable, RestoreFromTempTable疑似命令が付加されています。
  これにより、drop table, create table 後もデータが残ります。
  この機能は一時的に $$TableName のような一時テーブルを作成します。
*/

-- イベント情報詳細
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

-- イベント情報概要
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

-- コードマスタ
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
comment on column tran_event_info_detail.event_code is 'イベントコード';
comment on column tran_event_info_detail.info_type is '情報種別';
comment on column tran_event_info_detail.data is '内容';
comment on column tran_event_info_detail.insert_date is '登録日時';
comment on column tran_event_info_detail.insert_user is '登録者';
comment on column tran_event_info_detail.update_date is '更新日時';
comment on column tran_event_info_detail.update_user is '更新者';

comment on table tran_event_overview is 'イベント情報概要';
comment on column tran_event_overview.event_id is 'イベントコード';
comment on column tran_event_overview.event_entry_date is 'イベント登録日時';
comment on column tran_event_overview.event_name is 'イベント名';
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
