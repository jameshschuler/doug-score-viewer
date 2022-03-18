<template>
  <div class="notification">
    <button v-if="dismissible" class="delete"></button>
    <div class="icon-text mb-1">
      <span class="icon" :class="notificationTheme">
        <i class="fa-solid fa-fw fa-lg" :class="getNotificationIcon(notificationType)"></i>
      </span>
      <span class="is-size-5 has-text-weight-semibold" :class="notificationTheme">{{ notificationTitle }}</span>
    </div>
    <p class="block has-text-weight-semibold">{{ appError!.message }}</p>
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import { AppError } from "../models/common";
import { AppErrorType } from "../models/enums/error";
import { NotificationType } from "../models/enums/notification";
const { appError, dismissible } = defineProps({
  appError: Object,
  dismissible: Boolean,
});

const notificationType = ref<NotificationType>(getNotificationType(appError as AppError));
const notificationTheme = ref<string>(getNotificationTheme(notificationType.value));
const notificationTitle = ref<string>(NotificationType[notificationType.value]);

function getNotificationType(appError: AppError): NotificationType {
  switch (appError.errorType) {
    case AppErrorType.NotFound:
      return NotificationType.Info;
    case AppErrorType.BadRequest:
    case AppErrorType.Server:
      return NotificationType.Error;
    default:
      return NotificationType.Info;
  }
}

function getNotificationIcon(notificationType: NotificationType): string {
  switch (notificationType) {
    case NotificationType.Error:
      return "fa-car-crash";
    case NotificationType.Info:
      return "fa-circle-info";
    default:
      return "";
  }
}

function getNotificationTheme(notificationType: NotificationType): string {
  switch (notificationType) {
    case NotificationType.Error:
      return "has-text-danger";
    case NotificationType.Info:
      return "has-text-info";
    default:
      return "";
  }
}
</script>
<style lang="scss"></style>
