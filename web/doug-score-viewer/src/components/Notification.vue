<template>
  <div class="notification">
    <button v-if="dismissible" class="delete"></button>
    <div class="icon-text mb-1">
      <span class="icon" :class="notificationTheme">
        <i class="fa-solid fa-fw fa-lg" :class="getNotificationIcon(notificationType)"></i>
      </span>
      <span class="is-size-5 has-text-weight-semibold" :class="notificationTheme">{{ notificationTitle }}</span>
    </div>
    <p class="block has-text-weight-semibold">{{ message }}</p>
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import { NotificationType } from "../models/enums/notification";
const { dismissible, message, notificationType } = defineProps({
  message: String,
  dismissible: Boolean,
  notificationType: Number,
});

const notificationTheme = ref<string>(getNotificationTheme(notificationType as NotificationType));
const notificationTitle = ref<string>(NotificationType[notificationType as NotificationType]);

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
