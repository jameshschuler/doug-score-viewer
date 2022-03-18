<template>
  <div>
    <figure class="image is-32x32">
      <img :src="flagIconUrl" :alt="flagIconUrl" />
    </figure>
  </div>
</template>
<script setup lang="ts">
import { computed } from "vue";
import { getFlagIcon } from "../utils";
import { isNullEmptyOrWhitespace } from "../utils/strings";
const props = defineProps({
  originCountry: {
    required: true,
    type: String,
  },
});

const flagIconUrl = computed(() => {
  if (!isNullEmptyOrWhitespace(props.originCountry)) {
    return getFlagIcon(props.originCountry);
  }

  return props.originCountry;
});
</script>
<style lang="scss"></style>
